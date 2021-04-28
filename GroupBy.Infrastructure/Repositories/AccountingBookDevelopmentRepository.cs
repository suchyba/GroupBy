using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class AccountingBookDevelopmentRepository : IAccountingBookAsyncRepository
    {
        private static List<AccountingBook> accountingBooks;
        public AccountingBookDevelopmentRepository()
        {
            if(accountingBooks == null)
            {
                accountingBooks = new List<AccountingBook>
                {
                    new AccountingBook
                    {
                        Id = Guid.NewGuid(),
                        BookNumber = 0,
                        BookOrderNumber = 0,
                        Name = "Testowa",
                        Locked = false,
                        RelatedGroup = new Group
                        {
                            Id = Guid.NewGuid(),
                            Name = "test",
                            Description = "tak",
                        }
                    }
                };
            }
        }
        public async Task<AccountingBook> CreateAsync(AccountingBook domain)
        {
            accountingBooks.Add(domain);
            return domain;
        }

        public async Task DeleteAsync(Guid id)
        {
            AccountingBook book = accountingBooks.FirstOrDefault(ab => ab.Id == id);
            if (book == null)
                throw new NotFoundException("Accounting book", id);

            accountingBooks.Remove(book);
        }

        public async Task<AccountingBook> GetAsync(Guid id)
        {
            return accountingBooks.FirstOrDefault(ab => ab.Id == id);
        }

        public async Task<IEnumerable<AccountingBook>> GetAllAsync()
        {
            return accountingBooks;
        }

        public async Task<AccountingBook> UpdateAsync(AccountingBook domain)
        {
            AccountingBook book = accountingBooks.FirstOrDefault(ab => ab.Id == domain.Id);
            if (book == null)
                throw new NotFoundException("Accounting book", domain.Id);

            book.Locked = domain.Locked;
            book.Name = domain.Name;
            book.BookNumber = domain.BookNumber;
            book.BookOrderNumber = domain.BookOrderNumber;
            return book;
        }

        public async Task<bool> IsAccountingBookNumberUnique(int number)
        {
            return !accountingBooks.Any(a => a.BookNumber == number);
        }

        public async Task<bool> IsAccountingBookOrderNumberUnique(int number)
        {
            return !accountingBooks.Any(a => a.BookOrderNumber == number);
        }
    }
}
