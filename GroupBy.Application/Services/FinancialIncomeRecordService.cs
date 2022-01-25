using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.FinancialIncomeRecord;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class FinancialIncomeRecordService : AsyncService<FinancialIncomeRecord, FinancialIncomeRecordDTO, FinancialIncomeRecordDTO, FinancialIncomeRecordCreateDTO, FinancialIncomeRecordUpdateDTO>, IFinancialIncomeRecordService
    {
        public FinancialIncomeRecordService(
            IFinancialIncomeRecordRepository repository,
            IMapper mapper,
            IValidator<FinancialIncomeRecordUpdateDTO> updateValidator,
            IValidator<FinancialIncomeRecordCreateDTO> createValidator)
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
    }
}
