namespace GroupBy.Design.DTO.InventoryItemTransfer
{
    public class InventoryItemTransferSimpleDTO
    {
        public Guid Id { get; set; }
        public Guid OutcomeInventoryBookRecordId { get; set; }
        public Guid? IncomeInventoryBookRecordId { get; set; }
        public Guid SourceInventoryBookId { get; set; }
        public Guid DestinationInventoryBookId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public bool ConfirmedByReceiver { get; set; }
    }
}
