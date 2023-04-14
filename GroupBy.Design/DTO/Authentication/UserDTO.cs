using GroupBy.Design.TO.Volunteer;

namespace GroupBy.Design.TO.Authentication
{
    public class UserDTO
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public VolunteerDTO RelatedVolunteer { get; set; }
    }
}
