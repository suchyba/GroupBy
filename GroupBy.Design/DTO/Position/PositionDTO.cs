namespace GroupBy.Design.DTO.Position
{
    public class PositionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PositionSimpleDTO HigherPosition { get; set; }
    }
}
