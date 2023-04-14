using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryItemRepository : AsyncRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public override async Task<InventoryItem> UpdateAsync(InventoryItem domain)
        {
            InventoryItem item = await GetAsync(domain);
            if (item == null)
                throw new NotFoundException("InventoryItem", domain.Id);

            item.Name = domain.Name;
            item.Symbol = domain.Symbol;
            item.Description = domain.Description;
            item.Value = domain.Value;
            return item;
        }

        public async Task<IEnumerable<InventoryBookRecord>> GetInventoryItemHistoryAsync(Guid itemId, bool includeLocal = false)
        {
            InventoryItem item = await GetAsync(new { Id = itemId }, includeLocal, "History");
            if (item == null)
                throw new NotFoundException("InventoryItem", itemId);

            return item.History;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemWithoutHistory(bool includeLocal = false)
        {
            return (await GetAllAsync(includeLocal, "History"))
                .Where(i => i.History == null || i.History.Count() == 0);
        }
    }
}
