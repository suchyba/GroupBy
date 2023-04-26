using GroupBy.Design.DTO.Volunteer;

namespace GroupBy.Design.DTO.Authentication
{
    public class UserDTO
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public VolunteerDTO RelatedVolunteer { get; set; }
    }
}
