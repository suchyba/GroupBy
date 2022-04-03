using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Authentication;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            IVolunteerRepository volunteerRepository,
            IRegistrationCodeRepository registrationCodeRepository,
            IGroupRepository groupRepository,
            IRankRepository rankRepository,
            IValidator<RegisterDTO> registerValidator)
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
        }
        public async Task<AuthenticationResponseDTO> LoginUserAsync(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                throw new BadRequestException($"User {loginDTO.Email} not found");

            var result = await signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

            if (!result.Succeeded)
                throw new BadRequestException($"Credentials for {loginDTO.Email} are not valid");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            return new AuthenticationResponseDTO()
            {
                Email = loginDTO.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                VolunteerId = user.VolunteerId
            };
        }

        public async Task RegisterUserAsync(RegisterDTO registerDTO)
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
