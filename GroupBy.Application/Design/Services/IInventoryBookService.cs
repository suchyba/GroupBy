using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Application.DTO.InventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IInventoryBookService : IAsyncService<InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>
    {
        public Task<IEnumerable<InventoryBookRecordListDTO>> GetInventoryBookRecordsAsync(InventoryBookSimpleDTO inventoryBookDTO);
        public Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsAsync(InventoryBookSimpleDTO inventoryBookDTO);
    }
}
