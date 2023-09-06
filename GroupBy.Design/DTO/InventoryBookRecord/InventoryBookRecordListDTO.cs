using GroupBy.Design.DTO.Document;
using GroupBy.Design.DTO.InventoryBook;
using GroupBy.Design.DTO.InventoryItem;
using GroupBy.Design.DTO.InventoryItemSource;

namespace GroupBy.Design.DTO.InventoryBookRecord
{
    public class InventoryBookRecordListDTO
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public DocumentDTO Document { get; set; }
        public bool Income { get; set; }
        public InventoryItemSimpleDTO Item { get; set; }
        public InventoryItemSourceDTO Source { get; set; }
        public InventoryBookSimpleDTO Book { get; set; }
    }
}
