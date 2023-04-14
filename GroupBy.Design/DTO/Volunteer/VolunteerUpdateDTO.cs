namespace GroupBy.Design.TO.Volunteer
{
    public class VolunteerUpdateDTO
    {
        public Guid Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Confirmed { get; set; }
        public int? RankId { get; set; }
    }
}
