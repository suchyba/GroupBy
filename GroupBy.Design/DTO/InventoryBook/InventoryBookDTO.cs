using GroupBy.Design.DTO.Group;

namespace GroupBy.Design.DTO.InventoryBook
{
    public  class InventoryBookDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GroupSimpleDTO RelatedGroup { get; set; }
    }
}
