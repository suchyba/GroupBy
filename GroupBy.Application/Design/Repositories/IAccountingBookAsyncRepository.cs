using GroupBy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IAccountingBookAsyncRepository : IAsyncRepository<AccountingBook>
    {
        public Task<bool> IsAccountingBookNumberUnique(int number);
        public Task<bool> IsAccountingBookOrderNumberUnique(int number);
    }
}
