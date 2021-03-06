using GroupBy.Application.DTO.InventoryBookRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IInventoryBookRecordService : IAsyncService<InventoryBookRecordSimpleDTO, InventoryBookRecordSimpleDTO, InventoryBookRecordCreateDTO, InventoryBookRecordUpdateDTO>
    {
        public Task<IEnumerable<InventoryBookRecordDTO>> TransferItemAsync(InventoryBookRecordTransferDTO record);
    }
}
