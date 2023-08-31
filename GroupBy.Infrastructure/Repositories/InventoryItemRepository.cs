using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryItemRepository : AsyncRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<InventoryBookRecord>> GetInventoryItemHistoryAsync(Guid itemId, bool includeLocal = false)
        {
            InventoryItem item = await GetAsync(new { Id = itemId }, includeLocal,
                includes: new string[] {
                    "History.Document",
                    "History.Source",
                    "History.Book"
                });

            return item.History;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemWithoutHistory(bool includeLocal = false)
        {
            return (await GetAllAsync(includeLocal, "History"))
                .Where(i => i.History == null || i.History.Count() == 0);
        }

        protected override Expression<Func<InventoryItem, bool>> CompareKeys(object entity)
        {
            return i => entity.GetType().GetProperty("Id").GetValue(entity).Equals(i.Id);
        }
    }
}
