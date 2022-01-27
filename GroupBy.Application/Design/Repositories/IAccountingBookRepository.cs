using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IAccountingBookRepository : IAsyncRepository<AccountingBook>
    {
        public Task<bool> IsIdUnique(int bookNumber, int orderNumber);
        public Task<IEnumerable<FinancialRecord>> GetFinancialRecordsAsync(AccountingBook domain);
    }
}
