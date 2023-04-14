using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryItemSourceRepository : AsyncRepository<InventoryItemSource>, IInventoryItemSourceRepository
    {
        public InventoryItemSourceRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public override async Task<InventoryItemSource> UpdateAsync(InventoryItemSource domain)
        {
            var toModify = await GetAsync(domain);
            toModify.Name = domain.Name;

            return toModify;
        }
    }
}
