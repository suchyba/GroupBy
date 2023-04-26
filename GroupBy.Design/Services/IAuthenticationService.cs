using GroupBy.Design.DTO.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GroupBy.Design.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponseDTO> LoginUserAsync(LoginDTO loginDTO, string ipAddress);
        public Task RegisterUserAsync(RegisterDTO registerDTO, IUrlHelper urlHelper);
        public Task<UserDTO> GetUserAsync(string email);
        public Task ConfirmEmailAsync(string email, string token);
        public Task<AuthenticationResponseDTO> RefreshToken(string token, string ipAddress);
        public Task RevokeToken(string token, string ipAddress);
    }
}
