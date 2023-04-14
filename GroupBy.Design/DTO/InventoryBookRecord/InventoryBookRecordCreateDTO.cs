namespace GroupBy.Design.TO.InventoryBookRecord
{
    public class InventoryBookRecordCreateDTO
    {
        public Guid InventoryBookId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime Date { get; set; }
        public Guid DocumentId { get; set; }
        public bool Income { get; set; }
        public Guid SourceId { get; set; }
    }
}
