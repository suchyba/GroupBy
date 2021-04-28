using GroupBy.Domain;
using GroupBy.Application.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class AccountingBookRepository : IAccountingBookAsyncRepository
    {
        public async Task<AccountingBook> CreateAsync(AccountingBook domain)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountingBook> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountingBook>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AccountingBook> UpdateAsync(AccountingBook domain)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAccountingBookNumberUnique(int number)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAccountingBookOrderNumberUnique(int number)
        {
            throw new NotImplementedException();
        }
    }
}
