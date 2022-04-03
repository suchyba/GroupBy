using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Authentication
{
    public class AuthenticationResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public int VolunteerId { get; set; }
    }
}
