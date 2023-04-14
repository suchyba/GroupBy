using GroupBy.Design.TO.FinancialIncomeRecord;

namespace GroupBy.Design.Services
{
    public interface IFinancialIncomeRecordService : IAsyncService<FinancialIncomeRecordSimpleDTO, FinancialIncomeRecordDTO, FinancialIncomeRecordCreateDTO, FinancialIncomeRecordUpdateDTO>
    {

    }
}
