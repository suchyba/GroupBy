using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IAccountingBookRepository : IAsyncRepository<AccountingBook>
    {
        public Task<bool> IsIdUnique(int bookNumber, int orderNumber);
        public Task<IEnumerable<FinancialRecord>> GetFinancialRecordsAsync(AccountingBook domain, bool includeLocal = false);
    }
}
