namespace GroupBy.Design.TO.Position
{
    public class PositionSimpleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? HigherPositionId { get; set; }
    }
}
