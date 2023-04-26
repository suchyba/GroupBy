using GroupBy.Design.DTO.FinancialIncomeRecord;

namespace GroupBy.Design.Services
{
    public interface IFinancialIncomeRecordService : IAsyncService<FinancialIncomeRecordSimpleDTO, FinancialIncomeRecordDTO, FinancialIncomeRecordCreateDTO, FinancialIncomeRecordUpdateDTO>
    {

    }
}
