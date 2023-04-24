namespace GroupBy.Design.TO.Resolution
{
    public class ResolutionDTO
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Guid LegislatorId { get; set; }
        public Guid GroupId { get; set; }
    }
}
