using GroupBy.Data.Models;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Services
{
    public class AccountingBookService : IAccountingBookService
    {
        private readonly IAccountingBookRepository accountingBookRepository;

        public AccountingBookService(IAccountingBookRepository accountingBookRepository)
        {
            this.accountingBookRepository = accountingBookRepository;
        }
        public AccountingBook Create(AccountingBook domain)
        {
            return accountingBookRepository.Create(domain);
        }

        public bool Delete(AccountingBook domain)
        {
            return accountingBookRepository.Delete(domain);
        }

        public AccountingBook Get(AccountingBook domain)
        {
            return accountingBookRepository.Get(domain);
        }

        public IEnumerable<AccountingBook> GetAll()
        {
            return accountingBookRepository.GetAll();
        }

        public bool Update(AccountingBook domain)
        {
            return accountingBookRepository.Update(domain);
        }
    }
}
