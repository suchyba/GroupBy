using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.FinancialRecord;

namespace GroupBy.Design.Services
{
    public interface IAccountingBookService : IAsyncService<AccountingBookSimpleDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookSimpleDTO>
    {
        public Task<IEnumerable<FinancialRecordSimpleDTO>> GetFinancialRecordsAsync(AccountingBookSimpleDTO domain);
    }
}
