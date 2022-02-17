using GroupBy.Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponseDTO> LoginUserAsync(LoginDTO loginDTO);
        public Task RegisterUserAsync(RegisterDTO registerDTO);
    }
}
