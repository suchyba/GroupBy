using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class AccountingBookRepository : AsyncRepository<AccountingBook>, IAccountingBookRepository
    {
        public AccountingBookRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<bool> IsIdUnique(int bookNumber, int orderNumber)
        {
            if (!(await GetAllAsync()).Any(book => book.BookIdentificator == bookNumber))
                return true;

            return !(await GetAllAsync()).Any(book => book.BookIdentificator == bookNumber && book.BookOrderNumberId == orderNumber);
        }

        public async Task<IEnumerable<FinancialRecord>> GetFinancialRecordsAsync(AccountingBook domain, bool includeLocal = false)
        {
            return (await GetAsync(domain, includeLocal: includeLocal, includes: new string[] { "Records.RelatedDocument", "Records.RelatedProject", "Records.Values.Category" })).Records;
        }

        protected override Expression<Func<AccountingBook, bool>> CompareKeys(object entity)
        {
            return b => entity.GetType().GetProperty("Id").GetValue(entity).Equals(b.Id);
        }
    }
}
