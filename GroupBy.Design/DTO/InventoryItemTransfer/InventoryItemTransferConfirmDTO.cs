namespace GroupBy.Design.DTO.InventoryItemTransfer
{
    public class InventoryItemTransferConfirmDTO
    {
        public Guid Id { get; set; }
        public Guid InventoryItemSourceId { get; set; }
        public DateTime ConfirmationDateTime { get; set; }
    }
}
