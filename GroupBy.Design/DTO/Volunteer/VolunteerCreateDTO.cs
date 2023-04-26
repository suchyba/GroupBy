namespace GroupBy.Design.DTO.Volunteer
{
    public class VolunteerCreateDTO
    {
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Confirmed { get; set; }
        public Guid? RankId { get; set; }
    }
}
