using GroupBy.Design.DTO.Rank;

namespace GroupBy.Design.DTO.Volunteer
{
    public class VolunteerDTO
    {
        public Guid Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Confirmed { get; set; }
        public RankSimpleDTO Rank { get; set; }
    }
}
