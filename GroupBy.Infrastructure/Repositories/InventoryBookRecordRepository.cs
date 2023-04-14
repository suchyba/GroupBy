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
    public class InventoryBookRecordRepository : AsyncRepository<InventoryBookRecord>, IInventoryBookRecordRepository
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly IInventoryItemSourceRepository inventoryItemSourceRepository;
        private readonly IInventoryBookRepository inventoryBookRepository;

        public InventoryBookRecordRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IDocumentRepository documentRepository,
            IInventoryItemRepository inventoryItemRepository,
            IInventoryItemSourceRepository inventoryItemSourceRepository,
            IInventoryBookRepository inventoryBookRepository) : base(dBcontextLocator)
        {
            this.documentRepository = documentRepository;
            this.inventoryItemRepository = inventoryItemRepository;
            this.inventoryItemSourceRepository = inventoryItemSourceRepository;
            this.inventoryBookRepository = inventoryBookRepository;
        }

        public override async Task<InventoryBookRecord> UpdateAsync(InventoryBookRecord domain)
        {
            var record = await GetAsync(domain);
            record.Date = domain.Date;
            record.Income = domain.Income;

            Guid documentId = domain.Document.Id;
            domain.Document = await documentRepository.GetAsync(domain.Document);
            if (domain.Document == null)
                throw new NotFoundException("Document", documentId);

            Guid itemId = domain.Item.Id;
            domain.Item = await inventoryItemRepository.GetAsync(domain.Item);
            if (domain.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            Guid sourceId = domain.Source.Id;
            domain.Source = await inventoryItemSourceRepository.GetAsync(domain.Source);
            if (domain.Source == null)
                throw new NotFoundException("InventoryItemSource", sourceId);

            return record;
        }

        public override async Task<InventoryBookRecord> CreateAsync(InventoryBookRecord domain)
        {
            Guid bookId = domain.Book.Id;
            domain.Book = await inventoryBookRepository.GetAsync(domain.Book, false, "Records.Item");
            if (domain.Book == null)
                throw new NotFoundException("InventoryBook", bookId);

            Guid itemId = domain.Item.Id;
            domain.Item = await inventoryItemRepository.GetAsync(domain.Item);
            if (domain.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            if (!domain.Income
                && domain.Book.Records
                .OrderBy(r => r.Date)
                .Where(r => r.Item == domain.Item)
                .LastOrDefault()?.Income != true)
                throw new BadRequestException("You cannot remove item what is not in the inventory book");

            Guid sourceId = domain.Source.Id;
            domain.Source = await inventoryItemSourceRepository.GetAsync(domain.Source);
            if (domain.Source == null)
                throw new NotFoundException("InventoryItemSource", sourceId);

            Guid documentId = domain.Document.Id;
            domain.Document = await documentRepository.GetAsync(domain.Document);
            if (domain.Document == null)
                throw new NotFoundException("Document", documentId);

            var created = await context.Set<InventoryBookRecord>().AddAsync(domain);

            return created.Entity;
        }

        public async Task<IEnumerable<InventoryBookRecord>> TransferItemAsync(InventoryBookRecord inventoryBookFromRecord, InventoryBookRecord inventoryBookToRecord)
        {
            // Book from
            Guid bookFromId = inventoryBookFromRecord.Book.Id;
            inventoryBookFromRecord.Book = await inventoryBookRepository.GetAsync(inventoryBookFromRecord.Book, false, "Records.Item", "RelatedGroup");
            if (inventoryBookFromRecord.Book == null)
                throw new NotFoundException("InventoryFromBook", bookFromId);

            // Book to
            Guid bookToId = inventoryBookToRecord.Book.Id;
            inventoryBookToRecord.Book = await inventoryBookRepository.GetAsync(inventoryBookToRecord.Book, false, "Records.Item", "RelatedGroup");
            if (inventoryBookToRecord.Book == null)
                throw new NotFoundException("InventoryToBook", bookToId);

            // Item
            Guid itemId = inventoryBookFromRecord.Item.Id;
            inventoryBookFromRecord.Item = await inventoryItemRepository.GetAsync(inventoryBookFromRecord.Item.Id);
            inventoryBookToRecord.Item = await inventoryItemRepository.GetAsync(inventoryBookFromRecord.Item.Id);
            if (inventoryBookFromRecord.Item == null)
                throw new NotFoundException("InventoryItem", itemId);

            if (inventoryBookFromRecord.Book.Records
                .OrderBy(r => r.Date)
                .Where(r => r.Item == inventoryBookFromRecord.Item)
                .LastOrDefault()?.Income != true)
                throw new BadRequestException("You cannot remove item what is not in the inventory book");

            // Source from
            Guid sourceFromId = inventoryBookFromRecord.Source.Id;
            inventoryBookFromRecord.Source = await inventoryItemSourceRepository.GetAsync(inventoryBookFromRecord.Source);
            if (inventoryBookFromRecord.Source == null)
                throw new NotFoundException("InventoryItemFromSource", sourceFromId);

            // Source to
            Guid sourceToId = inventoryBookToRecord.Source.Id;
            inventoryBookToRecord.Source = await inventoryItemSourceRepository.GetAsync(inventoryBookToRecord.Source);
            if (inventoryBookToRecord.Source == null)
                throw new NotFoundException("InventoryItemToSource", sourceToId);

            // Document
            var document = await context.Set<Document>()
                .AddAsync(new Document
                {
                    Name = inventoryBookFromRecord.Document.Name,
                    FilePath = "",
                    Groups = new List<Group>() { inventoryBookFromRecord.Book.RelatedGroup, inventoryBookToRecord.Book.RelatedGroup },
                    RelatedProject = null
                });


            if (document == null)
                throw new BadRequestException($"Cannot create document {inventoryBookFromRecord.Document.Name}");

            inventoryBookFromRecord.Document = document.Entity;
            inventoryBookToRecord.Document = document.Entity;

            var createdFrom = await context.Set<InventoryBookRecord>().AddAsync(inventoryBookFromRecord);
            var createdTo = await context.Set<InventoryBookRecord>().AddAsync(inventoryBookToRecord);

            return new List<InventoryBookRecord>() { createdFrom.Entity, createdTo.Entity };
        }
    }
}
