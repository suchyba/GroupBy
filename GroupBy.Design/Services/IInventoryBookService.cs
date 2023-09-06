using GroupBy.Design.DTO.InventoryBook;
using GroupBy.Design.DTO.InventoryBookRecord;
using GroupBy.Design.DTO.InventoryItem;
using GroupBy.Design.DTO.InventoryItemTransfer;

namespace GroupBy.Design.Services
{
    public interface IInventoryBookService : IAsyncService<InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>
    {
        public Task<IEnumerable<InventoryBookRecordListDTO>> GetInventoryBookRecordsAsync(InventoryBookSimpleDTO inventoryBookDTO);
        public Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsAsync(InventoryBookSimpleDTO inventoryBookDTO);
        public Task<IEnumerable<InventoryItemTransferSimpleDTO>> GetIncomingInventoryItemTransfersAsync(InventoryBookSimpleDTO inventoryBookDTO, bool notConfirmedOnly = true);
    }
}
