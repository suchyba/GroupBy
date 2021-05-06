using GroupBy.Domain.Entities;
using GroupBy.Application.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupBy.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using GroupBy.Application.Exceptions;

namespace GroupBy.Data.Repositories
{
    public class AccountingBookRepository : AsyncRepository<AccountingBook>, IAccountingBookRepository
    {
        public AccountingBookRepository(GroupByDbContext context) : base(context)
        {

        }

        public override async Task DeleteAsync(AccountingBook domain)
        {
            var book = await GetAsync(domain);
            if (book == null)
                throw new NotFoundException("AccountingBook", new { domain.BookId, domain.BookOrderNumberId });

            context.Set<AccountingBook>().Remove(book);
            await context.SaveChangesAsync();
        }

        public override async Task<AccountingBook> GetAsync(AccountingBook domain)
        {
            var book = await context.Set<AccountingBook>().FirstOrDefaultAsync(book => book.BookId == domain.BookId && book.BookOrderNumberId == domain.BookOrderNumberId);
            if (book == null)
                throw new NotFoundException("AccountingBook", new { domain.BookId, domain.BookOrderNumberId });
            return book;
        }
        public override async Task<AccountingBook> CreateAsync(AccountingBook domain)
        {
            int key = domain.RelatedGroup.Id;
            domain.RelatedGroup = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == domain.RelatedGroup.Id);
            if (domain.RelatedGroup == null)
                throw new NotFoundException("Group", key);

            return await base.CreateAsync(domain);
        }
        public async Task<bool> IsIdUnique(int bookNumber, int orderNumber)
        {
            if (!(await context.Set<AccountingBook>().Where(book => book.BookId == bookNumber).AnyAsync()))
                return true;

            return !(await context.Set<AccountingBook>().Where(book => book.BookId == bookNumber && book.BookOrderNumberId == orderNumber).AnyAsync());
        }

        public override async Task<AccountingBook> UpdateAsync(AccountingBook domain)
        {
            var book = await context.Set<AccountingBook>().FirstOrDefaultAsync(ab => ab.BookId == domain.BookId && ab.BookOrderNumberId == domain.BookOrderNumberId);
            if (book == null)
                throw new NotFoundException("AccountingBook", new { domain.BookId, domain.BookOrderNumberId });

            book.Locked = domain.Locked;
            book.Name = domain.Name;

            await context.SaveChangesAsync();

            return book;
        }

        public Task<IEnumerable<FinancialRecord>> GetFinancialRecords()
        {
            throw new NotImplementedException();
        }
    }
}
