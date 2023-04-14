using GroupBy.Design.TO.InventoryBookRecord;
using GroupBy.Design.TO.InventoryItem;

namespace GroupBy.Design.Services
{
    public interface IInventoryItemService : IAsyncService<InventoryItemSimpleDTO, InventoryItemSimpleDTO, InventoryItemCreateDTO, InventoryItemSimpleDTO>
    {
        public Task<IEnumerable<InventoryBookRecordSimpleDTO>> GetInventoryItemHistoryAsync(Guid inventoryItemId);
        public Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsWithoutHistoryAsync();
    }
}
