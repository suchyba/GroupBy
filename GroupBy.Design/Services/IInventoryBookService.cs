using GroupBy.Design.DTO.InventoryBook;
using GroupBy.Design.DTO.InventoryBookRecord;
using GroupBy.Design.DTO.InventoryItem;

namespace GroupBy.Design.Services
{
    public interface IInventoryBookService : IAsyncService<InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>
    {
        public Task<IEnumerable<InventoryBookRecordListDTO>> GetInventoryBookRecordsAsync(InventoryBookSimpleDTO inventoryBookDTO);
        public Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsAsync(InventoryBookSimpleDTO inventoryBookDTO);
    }
}
