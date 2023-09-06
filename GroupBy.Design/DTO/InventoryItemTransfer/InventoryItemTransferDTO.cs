using GroupBy.Design.DTO.InventoryBook;
using GroupBy.Design.DTO.InventoryBookRecord;

namespace GroupBy.Design.DTO.InventoryItemTransfer
{
    public class InventoryItemTransferDTO
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public InventoryBookRecordSimpleDTO OutcomeInventoryBookRecord { get; set; }
        public InventoryBookRecordSimpleDTO IncomeInventoryBookRecord { get; set; }
        public InventoryBookSimpleDTO SourceInventoryBook { get; set; }
        public InventoryBookSimpleDTO DestinationInventoryBook { get; set; }
        public bool ConfirmedByReceiver { get; set; }
    }
}
