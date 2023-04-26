namespace GroupBy.Design.DTO.Position
{
    public class PositionCreateDTO
    {
        public string Name { get; set; }
        public Guid? HigherPositionId { get; set; }
    }
}
