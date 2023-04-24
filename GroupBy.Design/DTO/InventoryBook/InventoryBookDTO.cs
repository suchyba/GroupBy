using GroupBy.Design.TO.Group;

namespace GroupBy.Design.TO.InventoryBook
{
    public  class InventoryBookDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GroupSimpleDTO RelatedGroup { get; set; }
    }
}
