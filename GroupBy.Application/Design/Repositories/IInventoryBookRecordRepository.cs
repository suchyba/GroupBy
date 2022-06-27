using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IInventoryBookRecordRepository : IAsyncRepository<InventoryBookRecord>
    {
        public Task<IEnumerable<InventoryBookRecord>> TransferItemAsync(InventoryBookRecord inventoryBookFromRecord, InventoryBookRecord inventoryBookToRecord);
    }
}
