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
    public class InventoryBookRepository : AsyncRepository<InventoryBook>, IInventoryBookRepository
    {
        public InventoryBookRepository(DbContext context) : base(context)
        {

        }
        public override async Task<InventoryBook> CreateAsync(InventoryBook domain)
        {
            var groupId = domain.RelatedGroup.Id;
            domain.RelatedGroup = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == groupId);
            if (domain.RelatedGroup == null)
                throw new NotFoundException("Group", groupId);

            var createdBook = await context.Set<InventoryBook>().AddAsync(domain);
            await context.SaveChangesAsync();
            return createdBook.Entity;
        }
        public override async Task<InventoryBook> GetAsync(InventoryBook domain)
        {
            var book = await context.Set<InventoryBook>().FirstOrDefaultAsync(b => b.Id == domain.Id);
            if (book == null)
                throw new NotFoundException("InventoryBook", domain.Id);
            return book;
        }

        public override async Task<InventoryBook> UpdateAsync(InventoryBook domain)
        {
            var toModify = await GetAsync(domain);
            var groupId = domain.RelatedGroup.Id;
            Group relatedGroup = null;
            relatedGroup = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == groupId);
            if (relatedGroup == null)
                throw new NotFoundException("Group", groupId);
            toModify.RelatedGroup = relatedGroup;

            await context.SaveChangesAsync();
            return toModify;
        }
    }
}
