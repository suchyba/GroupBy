using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Authentication;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            IVolunteerRepository volunteerRepository,
            IRegistrationCodeRepository registrationCodeRepository,
            IGroupRepository groupRepository,
            IRankRepository rankRepository,
            IValidator<RegisterDTO> registerValidator,
            IEmailService emailService,
            IApplicationUserRepository applicationUserRepository,
            IRefreshTokenRepository refreshTokenRepository)
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
            this.applicationUserRepository = applicationUserRepository;
            this.refreshTokenRepository = refreshTokenRepository;
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

        public async Task<AuthenticationResponseDTO> LoginUserAsync(LoginDTO loginDTO, string ipAddress)
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
            RefreshToken refreshToken = await GenerateRefreshToken(ipAddress);

            await applicationUserRepository.AddRefreshTokenAsync(user.Email, refreshToken);

            await applicationUserRepository.RemoveOldRefreshTokens(user.Email, int.Parse(configuration["JWT:RefreshTokenTTLInDays"]));

            return new AuthenticationResponseDTO()
            {
                Email = loginDTO.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                VolunteerId = user.VolunteerId,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthenticationResponseDTO> RefreshToken(string token, string ipAddress)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken.IsRevoked)
            {
                await revokeDescendantRefreshTokens(refreshToken, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
            }

            if (!refreshToken.IsActive)
                throw new BadRequestException($"Refresh token {refreshToken.Token} is not active");

            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);

            await applicationUserRepository.AddRefreshTokenAsync(refreshToken.Owner.Email, newRefreshToken);

            await applicationUserRepository.RemoveOldRefreshTokens(refreshToken.Owner.Email, int.Parse(configuration["JWT:RefreshTokenTTLInDays"]));

            var jwtToken = await GenerateToken(refreshToken.Owner);

            return new AuthenticationResponseDTO()
            {
                Email = refreshToken.Owner.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                VolunteerId = refreshToken.Owner.VolunteerId,
                RefreshToken = newRefreshToken.Token
            };
        }

        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = await GenerateRefreshToken(ipAddress);
            await revokeRefreshToken(refreshToken.Token, ipAddress, "Replaced by new token", newRefreshToken);
            return newRefreshToken;
        }

        private async Task revokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
        {
            if (refreshToken.ReplacedByToken != null)
            {
                if (refreshToken.ReplacedByToken.IsActive)
                    await revokeRefreshToken(refreshToken.ReplacedByToken.Token, ipAddress, reason);
                else
                {
                    var childToken = await refreshTokenRepository.GetByTokenAsync(refreshToken.ReplacedByToken.Token);
                    await revokeDescendantRefreshTokens(childToken, ipAddress, reason);
                }
            }
        }

        private async Task revokeRefreshToken(string token, string ipAddress, string reason = null, RefreshToken replacedByToken = null)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(token);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIP = ipAddress;
            refreshToken.ReasonRevoked = reason;
            refreshToken.ReplacedByToken = replacedByToken;

            await refreshTokenRepository.UpdateAsync(refreshToken);
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

            var templatePath = configuration.GetValue<string>("ConfirmEmailTemplatePath");

            string emailTemplate = File.ReadAllText(templatePath);

            emailTemplate = emailTemplate.Replace(@"{CONFIRM_URL}", registerDTO.UrlToVerifyEmail + "?" + urlToConfirm.ToString());

            await emailService.SendEmailAsync(applicationUser.Email, "Confirm email", emailTemplate);
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(token);

            if (!refreshToken.IsActive)
                throw new BadRequestException($"Refresh token {token} is not active");

            await revokeRefreshToken(token, ipAddress, "Revoked without replacement");
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
        private async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            string token = null;
            while (token == null)
            {
                token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                var refreshToken = await refreshTokenRepository.GetAsync(new RefreshToken { Token = token });
                if (refreshToken == null)
                    token = null;
            }

            return new RefreshToken
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(double.Parse(configuration["JWT:RefreshTokenDurationInDays"])),
                Created = DateTime.UtcNow,
                CreatedByIP = ipAddress
            };
        }
    }
}
