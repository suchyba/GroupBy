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

        public override Task DeleteAsync(AccountingBook domain)
        {
            throw new NotImplementedException();
        }

        public override Task<AccountingBook> GetAsync(AccountingBook domain)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAccountingBookNumberUnique(int number)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAccountingBookOrderNumberUnique(int bookNumber, int orderNumber)
        {
            throw new NotImplementedException();
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

    }
}
