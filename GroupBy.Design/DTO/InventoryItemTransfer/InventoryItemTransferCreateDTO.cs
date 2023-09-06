namespace GroupBy.Design.DTO.InventoryItemTransfer
{
    public class InventoryItemTransferCreateDTO
    {
        public Guid SourceInventoryBookId { get; set; }
        public Guid DestinationInventoryBookId { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid SourceInventoryItemSourceId { get; set; }
        public Guid DocumentId { get; set; }
        public Guid InventoryItemId { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
