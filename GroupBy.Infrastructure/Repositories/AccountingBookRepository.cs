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
    public class AccountingBookRepository : AsyncRepository<AccountingBook>, IAccountingBookRepository
    {
        private readonly IGroupRepository groupRepository;

        public AccountingBookRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator, IGroupRepository groupRepository) : base(dBcontextLocator)
        {
            this.groupRepository = groupRepository;
        }

        public override async Task DeleteAsync(AccountingBook domain)
        {
            var book = await GetAsync(domain);
            if (book == null)
                throw new NotFoundException("AccountingBook", new { domain.BookId, domain.BookOrderNumberId });

            context.Set<AccountingBook>().Remove(book);
            await context.SaveChangesAsync();
        }

        public override async Task<AccountingBook> CreateAsync(AccountingBook domain)
        {
            Guid key = domain.RelatedGroup.Id;
            domain.RelatedGroup = await groupRepository.GetAsync(domain.RelatedGroup);
            if (domain.RelatedGroup == null)
                throw new NotFoundException("Group", key);

            domain.Records = new List<FinancialRecord>();

            return await base.CreateAsync(domain);
        }
        public async Task<bool> IsIdUnique(int bookNumber, int orderNumber)
        {
            if (!(await GetAllAsync()).Any(book => book.BookId == bookNumber))
                return true;

            return !(await GetAllAsync()).Any(book => book.BookId == bookNumber && book.BookOrderNumberId == orderNumber);
        }

        public override async Task<AccountingBook> UpdateAsync(AccountingBook domain)
        {
            var book = await GetAsync(domain);
            if (book == null)
                throw new NotFoundException("AccountingBook", new { domain.BookId, domain.BookOrderNumberId });

            book.Locked = domain.Locked;
            book.Name = domain.Name;

            return book;
        }

        public async Task<IEnumerable<FinancialRecord>> GetFinancialRecordsAsync(AccountingBook domain, bool includeLocal = false)
        {
            return (await GetAsync(domain, includeLocal, "Records")).Records;
        }
    }
}
