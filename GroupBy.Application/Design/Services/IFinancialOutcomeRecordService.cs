using GroupBy.Application.DTO.FinancialOutcomeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IFinancialOutcomeRecordService : IAsyncService<FinancialOutcomeRecordDTO, FinancialOutcomeRecordDTO, FinancialOutcomeRecordCreateDTO, FinancialOutcomeRecordUpdateDTO>
    {

    }
}
