using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryBookRepository : AsyncRepository<InventoryBook>, IInventoryBookRepository
    {
        public InventoryBookRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<InventoryBookRecord>> GetInventoryBookRecordsAsync(InventoryBook book, bool includeLocal = false)
        {
            return (await GetAsync(book, includeLocal, includes: new string[] { "Records.Item", "Records.Source", "Records.Document" })).Records;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync(InventoryBook book, bool includeLocal = false)
        {
            book = await GetAsync(book, includeLocal, includes: "Records.Item");

            List<InventoryItem> items = new();
            foreach (var record in book.Records)
            {
                if (record.Income)
                    items.Add(record.Item);
                else
                    items.Remove(record.Item);
            }

            return items;
        }

        protected override Expression<Func<InventoryBook, bool>> CompareKeys(object entity)
        {
            return b => entity.GetType().GetProperty("Id").GetValue(entity).Equals(b.Id);
        }
    }
}
