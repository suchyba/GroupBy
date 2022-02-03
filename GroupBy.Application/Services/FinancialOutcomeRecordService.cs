using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.FinancialOutcomeRecord;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class FinancialOutcomeRecordService : AsyncService<FinancialOutcomeRecord, FinancialOutcomeRecordSimpleDTO, FinancialOutcomeRecordDTO, FinancialOutcomeRecordCreateDTO, FinancialOutcomeRecordUpdateDTO>, IFinancialOutcomeRecordService
    {
        public FinancialOutcomeRecordService(
            IFinancialOutcomeRecordRepository repository,
            IMapper mapper,
            IValidator<FinancialOutcomeRecordUpdateDTO> updateValidator,
            IValidator<FinancialOutcomeRecordCreateDTO> createValidator)
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
    }
}
