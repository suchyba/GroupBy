using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Authentication;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GroupBy.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IRegistrationCodeRepository registrationCodeRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IRankRepository rankRepository;
        private readonly IValidator<RegisterDTO> registerValidator;
        private readonly IEmailService emailService;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            IVolunteerRepository volunteerRepository,
            IRegistrationCodeRepository registrationCodeRepository,
            IGroupRepository groupRepository,
            IRankRepository rankRepository,
            IValidator<RegisterDTO> registerValidator,
            IEmailService emailService,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.mapper = mapper;
            this.volunteerRepository = volunteerRepository;
            this.registrationCodeRepository = registrationCodeRepository;
            this.groupRepository = groupRepository;
            this.rankRepository = rankRepository;
            this.registerValidator = registerValidator;
            this.emailService = emailService;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task ConfirmEmailAsync(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new NotFoundException("User", new { Id = email });

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                throw new BadRequestException("Cannot confirm email address");
        }

        public async Task<UserDTO> GetUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new NotFoundException("User", new { Id = email });

            Guid volunteerId = user.VolunteerId;

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                user.RelatedVolunteer = await volunteerRepository.GetAsync(new Volunteer { Id = user.VolunteerId }, includes: "Rank");
            }

            if (user.RelatedVolunteer == null)
                throw new NotFoundException("Volunteer", new { Id = volunteerId });

            return mapper.Map<UserDTO>(user); ;
        }

        public async Task<AuthenticationResponseDTO> LoginUserAsync(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                throw new BadRequestException($"User {loginDTO.Email} not found");

            var result = await signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

            if (!result.Succeeded)
            {
                if (await userManager.IsEmailConfirmedAsync(user))
                    throw new BadRequestException($"Credentials for {loginDTO.Email} are not valid");

                else
                    throw new BadRequestException($"You have to verify your email address");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            return new AuthenticationResponseDTO()
            {
                Email = loginDTO.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                VolunteerId = user.VolunteerId
            };
        }

        public async Task RegisterUserAsync(RegisterDTO registerDTO, IUrlHelper urlHelper)
        {
            var validationResult = await registerValidator.ValidateAsync(registerDTO);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            ApplicationUser user = null;

            try
            {
                user = await userManager.FindByEmailAsync(registerDTO.Email);
            }
            catch (NotFoundException) { }

            if (user != null)
                throw new BadRequestException("Account with this email address already exists");

            var applicationUser = mapper.Map<ApplicationUser>(registerDTO);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var registrationCode = await registrationCodeRepository.GetAsync(
                    new RegistrationCode
                    {
                        Code = registerDTO.RegistrationCode
                    },
                    includeLocal: false,
                    includes: new string[] {"TargetRank", "TargetGroup" });

                applicationUser.RelatedVolunteer.Rank = await rankRepository.GetAsync(registrationCode.TargetRank);
                applicationUser.RelatedVolunteer.Confirmed = true;

                var volunteer = await volunteerRepository.CreateAsync(applicationUser.RelatedVolunteer);

                await groupRepository.AddMemberAsync(registrationCode.TargetGroup.Id, volunteer, includeLocal: true);

                applicationUser.UserName = applicationUser.Email;

                var result = await userManager.CreateAsync(applicationUser, registerDTO.Password);
                if (!result.Succeeded)
                    throw new Exception($"{result.Errors}");

                var token = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

                var urlToConfirm = HttpUtility.ParseQueryString(string.Empty);
                urlToConfirm.Add("token", token);
                urlToConfirm.Add("email", applicationUser.Email);

                var templatePath = configuration.GetValue<string>("ConfirmEmailTemplatePath");

                string emailTemplate = File.ReadAllText(templatePath);

                emailTemplate = emailTemplate.Replace(@"{CONFIRM_URL}", registerDTO.UrlToVerifyEmail + "?" + urlToConfirm.ToString());

                await emailService.SendEmailAsync(applicationUser.Email, "Confirm email", emailTemplate);
                await uow.Commit();
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("VolunteerId", user.VolunteerId.ToString())
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT:DurationInMinutes"])),
                claims: claims,
                signingCredentials: signingCredentials);
        }
    }
}
