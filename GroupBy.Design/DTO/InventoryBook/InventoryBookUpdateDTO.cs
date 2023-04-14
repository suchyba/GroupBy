namespace GroupBy.Design.TO.InventoryBook
{
    public class InventoryBookUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RelatedGroupId { get; set; }
    }
}
