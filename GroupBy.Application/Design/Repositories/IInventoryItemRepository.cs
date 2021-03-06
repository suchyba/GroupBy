using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IInventoryItemRepository : IAsyncRepository<InventoryItem>
    {
        public Task<IEnumerable<InventoryBookRecord>> GetInventoryItemHistoryAsync(int itemId);
        public Task<IEnumerable<InventoryItem>> GetInventoryItemWithoutHistory();
    }
}
