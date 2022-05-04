using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.FinancialRecord;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAccountingBookService : IAsyncService<AccountingBookSimpleDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookSimpleDTO>
    {
        public Task<IEnumerable<FinancialRecordSimpleDTO>> GetFinancialRecordsAsync(AccountingBookSimpleDTO domain);
    }
}
