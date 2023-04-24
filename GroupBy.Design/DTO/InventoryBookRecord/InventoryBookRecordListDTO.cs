using GroupBy.Design.TO.Document;
using GroupBy.Design.TO.InventoryItem;
using GroupBy.Design.TO.InventoryItemSource;

namespace GroupBy.Design.TO.InventoryBookRecord
{
    public class InventoryBookRecordListDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DocumentDTO Document { get; set; }
        public bool Income { get; set; }
        public InventoryItemSimpleDTO Item { get; set; }
        public InventoryItemSourceDTO Source { get; set; }
    }
}
