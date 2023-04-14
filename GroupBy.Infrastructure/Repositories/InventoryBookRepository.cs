using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class InventoryBookRepository : AsyncRepository<InventoryBook>, IInventoryBookRepository
    {
        private readonly IGroupRepository groupRepository;

        public InventoryBookRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator, IGroupRepository groupRepository) : base(dBcontextLocator)
        {
            this.groupRepository = groupRepository;
        }
        public override async Task<InventoryBook> CreateAsync(InventoryBook domain)
        {
            var groupId = domain.RelatedGroup.Id;
            domain.RelatedGroup = await groupRepository.GetAsync(domain.RelatedGroup, false, "InventoryBook");
            if (domain.RelatedGroup == null)
                throw new NotFoundException("Group", groupId);

            if (domain.RelatedGroup.InventoryBook != null)
                throw new BadRequestException("Inventory book exists in this group");

            var createdBook = await context.Set<InventoryBook>().AddAsync(domain);
            return createdBook.Entity;
        }

        public async Task<IEnumerable<InventoryBookRecord>> GetInventoryBookRecordsAsync(InventoryBook book, bool includeLocal = false)
        {
            return (await GetAsync(book, includeLocal, "Records")).Records;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync(InventoryBook book, bool includeLocal = false)
        {
            book = await GetAsync(book, includeLocal, "Records.Item");

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

        public override async Task<InventoryBook> UpdateAsync(InventoryBook domain)
        {
            var toModify = await GetAsync(domain);
            var groupId = domain.RelatedGroup.Id;
            Group relatedGroup = null;
            relatedGroup = await groupRepository.GetAsync(domain.RelatedGroup);
            if (relatedGroup == null)
                throw new NotFoundException("Group", groupId);
            toModify.RelatedGroup = relatedGroup;

            return toModify;
        }
    }
}
