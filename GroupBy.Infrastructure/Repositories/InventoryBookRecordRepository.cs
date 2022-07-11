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
            var record = await context.Set<InventoryBookRecord>().FirstOrDefaultAsync(r => r.Id == domain.Id);
            if (record == null)
                throw new NotFoundException("InventoryBookRecord", new { domain.Id });
            return record;
        }

        public override async Task<InventoryBookRecord> UpdateAsync(InventoryBookRecord domain)
        {
            var record = await GetAsync(domain);
            record.Date = domain.Date;
            record.Income = domain.Income;

            int documentId = domain.Document.Id;
            domain.Document = await context.Set<Document>().FirstOrDefaultAsync(d => d.Id == documentId);
            if (domain.Document == null)
                throw new NotFoundException("Document", documentId);

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
            int bookId = domain.InventoryBookId;
            domain.Book = await context.Set<InventoryBook>()
                .Include(b => b.Records)
                .ThenInclude(r => r.Item)
                .FirstOrDefaultAsync(i => i.Id == bookId);
            if (domain.Book == null)
                throw new NotFoundException("InventoryBook", bookId);

            int itemId = domain.Item.Id;
            domain.Item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == itemId);
            if (domain.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            if (!domain.Income 
                && domain.Book.Records
                .OrderBy(r => r.Date)
                .Where(r => r.Item == domain.Item)
                .LastOrDefault()?.Income != true)
                throw new BadRequestException("You cannot remove item what is not in the inventory book");

            int sourceId = domain.Source.Id;
            domain.Source = await context.Set<InventoryItemSource>().FirstOrDefaultAsync(s => s.Id == sourceId);
            if (domain.Source == null)
                throw new NotFoundException("InventoryItemSource", sourceId);

            int documentId = domain.Document.Id;
            domain.Document = await context.Set<Document>().FirstOrDefaultAsync(s => s.Id == documentId);
            if (domain.Document == null)
                throw new NotFoundException("Document", documentId);

            var created = await context.Set<InventoryBookRecord>().AddAsync(domain);

            await context.SaveChangesAsync();

            return created.Entity;
        }

        public async Task<IEnumerable<InventoryBookRecord>> TransferItemAsync(InventoryBookRecord inventoryBookFromRecord, InventoryBookRecord inventoryBookToRecord)
        {
            // Book from
            int bookFromId = inventoryBookFromRecord.InventoryBookId;
            inventoryBookFromRecord.Book = await context.Set<InventoryBook>()
                .Include(b => b.Records)
                .ThenInclude(r => r.Item)
                .Include(b => b.RelatedGroup)
                .FirstOrDefaultAsync(i => i.Id == bookFromId);
            if (inventoryBookFromRecord.Book == null)
                throw new NotFoundException("InventoryFromBook", bookFromId);

            // Book to
            int bookToId = inventoryBookToRecord.InventoryBookId;
            inventoryBookToRecord.Book = await context.Set<InventoryBook>()
                .Include(b => b.Records)
                .ThenInclude(r => r.Item)
                .Include(b => b.RelatedGroup)
                .FirstOrDefaultAsync(i => i.Id == bookToId);
            if (inventoryBookToRecord.Book == null)
                throw new NotFoundException("InventoryToBook", bookToId);

            // Item
            int itemId = inventoryBookFromRecord.Item.Id;
            inventoryBookFromRecord.Item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == itemId);
            inventoryBookToRecord.Item = await context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.Id == itemId);
            if (inventoryBookFromRecord.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            if (inventoryBookFromRecord.Book.Records
                .OrderBy(r => r.Date)
                .Where(r => r.Item == inventoryBookFromRecord.Item)
                .LastOrDefault()?.Income != true)
                throw new BadRequestException("You cannot remove item what is not in the inventory book");

            // Source from
            int sourceFromId = inventoryBookFromRecord.Source.Id;
            inventoryBookFromRecord.Source = await context.Set<InventoryItemSource>().FirstOrDefaultAsync(s => s.Id == sourceFromId);
            if (inventoryBookFromRecord.Source == null)
                throw new NotFoundException("InventoryItemFromSource", sourceFromId);

            // Source to
            int sourceToId = inventoryBookToRecord.Source.Id;
            inventoryBookToRecord.Source = await context.Set<InventoryItemSource>().FirstOrDefaultAsync(s => s.Id == sourceToId);
            if (inventoryBookToRecord.Source == null)
                throw new NotFoundException("InventoryItemToSource", sourceToId);

            // Document
            var document = await context.Set<Document>()
                .AddAsync(new Document { 
                    Name = inventoryBookFromRecord.Document.Name,
                    FilePath = "",
                    Groups = new List<Group>() { inventoryBookFromRecord.Book.RelatedGroup, inventoryBookToRecord.Book.RelatedGroup},
                    RelatedProject = null
                });


            if (document == null)
                throw new BadRequestException($"Cannot create document {inventoryBookFromRecord.Document.Name}");

            inventoryBookFromRecord.Document = document.Entity;
            inventoryBookToRecord.Document = document.Entity;

            var createdFrom = await context.Set<InventoryBookRecord>().AddAsync(inventoryBookFromRecord);
            var createdTo = await context.Set<InventoryBookRecord>().AddAsync(inventoryBookToRecord);

            await context.SaveChangesAsync();

            return new List<InventoryBookRecord>() { createdFrom.Entity, createdTo.Entity };
        }
    }
}
