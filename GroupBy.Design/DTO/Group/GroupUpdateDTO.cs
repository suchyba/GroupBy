namespace GroupBy.Design.DTO.Group
{
    public class GroupUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
}
