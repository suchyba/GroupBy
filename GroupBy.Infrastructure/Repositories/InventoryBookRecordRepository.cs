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
    public class InventoryBookRecordRepository : AsyncRepository<InventoryBookRecord>, IInventoryBookRecordRepository
    {
        public InventoryBookRecordRepository(DbContext context) : base(context)
        {

        }

        public override async Task<InventoryBookRecord> GetAsync(InventoryBookRecord domain)
        {
            var record = await context.Set<InventoryBookRecord>().FirstOrDefaultAsync(r => r.InventoryBookId == domain.InventoryBookId && r.Id == domain.Id);
            if (record == null)
                throw new NotFoundException("InventoryBookRecord", new { domain.Id, domain.InventoryBookId });
            return record;
        }

        public override async Task<InventoryBookRecord> UpdateAsync(InventoryBookRecord domain)
        {
            var record = await GetAsync(domain);
            record.Date = domain.Date;
            record.Income = domain.Income;
            record.Document = domain.Document;
            
            int itemId = domain.Item.Id;
            domain.Item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == itemId);
            if (domain.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            int sourceId = domain.Source.Id;
            domain.Source = await context.Set<InventoryItemSource>().FirstOrDefaultAsync(s => s.Id == sourceId);
            if (domain.Source == null)
                throw new NotFoundException("InventoryItemSource", sourceId);

            await context.SaveChangesAsync();

            return record;
        }

        public override async Task<InventoryBookRecord> CreateAsync(InventoryBookRecord domain)
        {
            if (context.Set<InventoryBookRecord>().Count() > 0)
                domain.Id = await context.Set<InventoryBookRecord>().MaxAsync(r => r.Id) + 1;
            else
                domain.Id = 1;

            int bookId = domain.InventoryBookId;
            domain.Book = await context.Set<InventoryBook>().FirstOrDefaultAsync(i => i.Id == bookId);
            if (domain.Book == null)
                throw new NotFoundException("InventoryBook", bookId);

            int itemId = domain.Item.Id;
            domain.Item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == itemId);
            if (domain.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            int sourceId = domain.Source.Id;
            domain.Source = await context.Set<InventoryItemSource>().FirstOrDefaultAsync(s => s.Id == sourceId);
            if (domain.Source == null)
                throw new NotFoundException("InventoryItemSource", sourceId);

            var created = await context.Set<InventoryBookRecord>().AddAsync(domain);

            await context.SaveChangesAsync();

            return created.Entity;
        }
    }
}
