namespace GroupBy.Design.TO.Group
{
    public class GroupCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentGroupId { get; set; }
        public int OwnerId { get; set; }
    }
}
