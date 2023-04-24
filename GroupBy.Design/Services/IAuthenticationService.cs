using GroupBy.Design.TO.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GroupBy.Design.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponseDTO> LoginUserAsync(LoginDTO loginDTO);
        public Task RegisterUserAsync(RegisterDTO registerDTO, IUrlHelper urlHelper);
        public Task<UserDTO> GetUserAsync(string email);
        public Task ConfirmEmailAsync(string email, string token);
    }
}
