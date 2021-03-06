using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryItemRepository : AsyncRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(DbContext context) : base(context)
        {

        }
        public override async Task<InventoryItem> GetAsync(InventoryItem domain)
        {
            InventoryItem item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == domain.Id);
            if (item == null)
                throw new NotFoundException("InventoryItem", domain.Id);
            return item;
        }

        public override async Task<InventoryItem> UpdateAsync(InventoryItem domain)
        {
            InventoryItem item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == domain.Id);
            if (item == null)
                throw new NotFoundException("InventoryItem", domain.Id);

            item.Name = domain.Name;
            item.Symbol = domain.Symbol;
            item.Description = domain.Description;
            item.Value = domain.Value;
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<InventoryBookRecord>> GetInventoryItemHistoryAsync(int itemId)
        {
            InventoryItem item = await context.Set<InventoryItem>()
                .Include(i => i.History)
                .FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null)
                throw new NotFoundException("InventoryItem", itemId);

            return item.History;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemWithoutHistory()
        {
            return context.Set<InventoryItem>()
                .Include(i => i.History)
                .Where(i => i.History == null || i.History.Count() == 0);
        }
    }
}
