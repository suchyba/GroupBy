using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class InventoryItemSourceRepository : AsyncRepository<InventoryItemSource>, IInventoryItemSourceRepository
    {
        public InventoryItemSourceRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<InventoryItemSource, bool>> CompareKeys(object entity)
        {
            return s => entity.GetType().GetProperty("Id").GetValue(entity).Equals(s.Id);
        }
    }
}
