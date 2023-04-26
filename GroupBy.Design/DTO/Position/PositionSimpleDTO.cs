namespace GroupBy.Design.DTO.Position
{
    public class PositionSimpleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? HigherPositionId { get; set; }
    }
}
