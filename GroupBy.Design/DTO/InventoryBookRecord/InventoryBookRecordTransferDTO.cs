namespace GroupBy.Design.DTO.InventoryBookRecord
{
    public class InventoryBookRecordTransferDTO
    {
        public Guid InventoryBookFromId { get; set; }
        public Guid InventoryBookToId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime Date { get; set; }
        public string DocumentName { get; set; }
        public Guid SourceFromId { get; set; }
        public Guid SourceToId { get; set; }
    }
}
