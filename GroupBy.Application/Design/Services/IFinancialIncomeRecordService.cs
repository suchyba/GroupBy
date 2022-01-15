using GroupBy.Application.DTO.FinancialIncomeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IFinancialIncomeRecordService : IAsyncService<FinancialIncomeRecordDTO, FinancialIncomeRecordCreateDTO, FinancialIncomeRecordUpdateDTO>
    {

    }
}
