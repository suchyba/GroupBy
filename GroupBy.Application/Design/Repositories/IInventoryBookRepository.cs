using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IInventoryBookRepository : IAsyncRepository<InventoryBook>
    {
        public Task<IEnumerable<InventoryBookRecord>> GetInventoryBookRecordsAsync(InventoryBook book);
        public Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync(InventoryBook book);
    }
}
