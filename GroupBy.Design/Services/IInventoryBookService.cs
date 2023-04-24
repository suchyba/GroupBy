using GroupBy.Design.TO.InventoryBook;
using GroupBy.Design.TO.InventoryBookRecord;
using GroupBy.Design.TO.InventoryItem;

namespace GroupBy.Design.Services
{
    public interface IInventoryBookService : IAsyncService<InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>
    {
        public Task<IEnumerable<InventoryBookRecordListDTO>> GetInventoryBookRecordsAsync(InventoryBookSimpleDTO inventoryBookDTO);
        public Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsAsync(InventoryBookSimpleDTO inventoryBookDTO);
    }
}
