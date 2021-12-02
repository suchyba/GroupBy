using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryItemSourceRepository : AsyncRepository<InventoryItemSource>, IInventoryItemSourceRepository
    {
        public InventoryItemSourceRepository(DbContext context) : base(context)
        {

        }

        public override async Task<InventoryItemSource> GetAsync(InventoryItemSource domain)
        {
            var source = await context.Set<InventoryItemSource>().FirstOrDefaultAsync(s => s.Id == domain.Id);
            if (source == null)
                throw new NotFoundException("InventoryItemSource", domain.Id);
            return source;
        }

        public override async Task<InventoryItemSource> UpdateAsync(InventoryItemSource domain)
        {
            var toModify = await GetAsync(domain);
            toModify.Name = domain.Name;

            await context.SaveChangesAsync();
            return toModify;
        }
    }
}
