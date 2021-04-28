using GroupBy.Core.Entities;
using GroupBy.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Infrastructure.Repositories
{
    public class AccountingBookDevelopmentRepository : IAccountingBookRepository
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
                        BookId = 0,
                        BookOrderNumberId = 0,
                        Name = "Testowa",
                        Locked = false,
                        RelatedGroup = new Group
                        {
                            Id = 0,
                            Name = "test",
                            Description = "tak",
                        }
                    }
                };
            }
        }
        public AccountingBook Create(AccountingBook domain)
        {
            accountingBooks.Add(domain);
            return domain;
        }

        public bool Delete(AccountingBook domain)
        {
            AccountingBook book = accountingBooks.FirstOrDefault(ab => ab.BookId == domain.BookId && ab.BookOrderNumberId == domain.BookOrderNumberId);
            if (book == null)
                return false;

            accountingBooks.Remove(book);
            return true;
        }

        public AccountingBook Get(AccountingBook domain)
        {
            return accountingBooks.FirstOrDefault(ab => ab.BookId == domain.BookId && ab.BookOrderNumberId == domain.BookOrderNumberId);
        }

        public IEnumerable<AccountingBook> GetAll()
        {
            return accountingBooks;
        }

        public bool Update(AccountingBook domain)
        {
            AccountingBook book = accountingBooks.FirstOrDefault(ab => ab.BookId == domain.BookId && ab.BookOrderNumberId == domain.BookOrderNumberId);
            if (book == null)
                return false;

            book.Locked = domain.Locked;
            book.Name = domain.Name;
            return true;
        }
    }
}
