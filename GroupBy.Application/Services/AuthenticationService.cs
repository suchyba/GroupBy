using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Authentication;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            IVolunteerRepository volunteerRepository,
            IRegistrationCodeRepository registrationCodeRepository,
            IGroupRepository groupRepository,
            IRankRepository rankRepository,
            IValidator<RegisterDTO> registerValidator,
            IEmailService emailService)
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

            int volunteerId = user.VolunteerId;
            user.RelatedVolunteer = await volunteerRepository.GetAsync(new Volunteer { Id = user.VolunteerId });

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
                throw new Exceptions.ValidationException(validationResult);

            var user = await userManager.FindByEmailAsync(registerDTO.Email);
            if (user != null)
                throw new BadRequestException("Account with this email address already exists");

            var applicationUser = mapper.Map<ApplicationUser>(registerDTO);

            var registrationCode = await registrationCodeRepository.GetAsync(new RegistrationCode { Code = registerDTO.RegistrationCode });

            applicationUser.RelatedVolunteer.Rank = registrationCode.TargetRank;
            var volunteer = await volunteerRepository.CreateAsync(applicationUser.RelatedVolunteer);

            await groupRepository.AddMemberAsync(registrationCode.TargetGroup.Id, volunteer.Id);

            applicationUser.UserName = applicationUser.Email;

            var result = await userManager.CreateAsync(applicationUser, registerDTO.Password);
            if (!result.Succeeded)
                throw new Exception($"{result.Errors}");

            volunteer.Confirmed = true;
            await volunteerRepository.UpdateAsync(volunteer);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

            var urlToConfirm = HttpUtility.ParseQueryString(string.Empty);
            urlToConfirm.Add("token", token);
            urlToConfirm.Add("email", applicationUser.Email);

            await emailService.SendEmailAsync(applicationUser.Email, "Confirm email", @"<a href=""" + registerDTO.UrlToVerifyEmail + "?" + urlToConfirm.ToString() + @""">click link </a>");
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
