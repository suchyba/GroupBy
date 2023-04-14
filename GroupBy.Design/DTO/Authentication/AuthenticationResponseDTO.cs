namespace GroupBy.Design.TO.Authentication
{
    public class AuthenticationResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public Guid VolunteerId { get; set; }
    }
}
