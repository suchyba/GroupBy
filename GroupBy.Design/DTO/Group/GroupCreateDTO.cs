namespace GroupBy.Design.TO.Group
{
    public class GroupCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentGroupId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
