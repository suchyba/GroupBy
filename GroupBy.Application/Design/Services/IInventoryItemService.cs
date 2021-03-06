using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Application.DTO.InventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IInventoryItemService : IAsyncService<InventoryItemSimpleDTO, InventoryItemSimpleDTO, InventoryItemCreateDTO, InventoryItemSimpleDTO>
    {
        public Task<IEnumerable<InventoryBookRecordSimpleDTO>> GetInventoryItemHistoryAsync(int inventoryItemId);
        public Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsWithoutHistoryAsync();
    }
}
