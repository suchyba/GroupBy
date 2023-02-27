using System.Text.Json.Serialization;

namespace GroupBy.Application.DTO.Authentication
{
    public class AuthenticationResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public int VolunteerId { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
