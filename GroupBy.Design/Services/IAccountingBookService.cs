using GroupBy.Design.DTO.AccountingBook;
using GroupBy.Design.DTO.FinancialRecord;

namespace GroupBy.Design.Services
{
    public interface IAccountingBookService : IAsyncService<AccountingBookSimpleDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookSimpleDTO>
    {
        public Task<IEnumerable<FinancialRecordSimpleDTO>> GetFinancialRecordsAsync(AccountingBookSimpleDTO domain);
    }
}
