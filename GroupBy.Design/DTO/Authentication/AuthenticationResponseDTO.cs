using System.Text.Json.Serialization;

namespace GroupBy.Design.DTO.Authentication
{
    public class AuthenticationResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public Guid VolunteerId { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
