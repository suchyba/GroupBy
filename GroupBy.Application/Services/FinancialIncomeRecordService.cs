using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.FinancialIncomeRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class FinancialIncomeRecordService : AsyncService<FinancialIncomeRecord, FinancialIncomeRecordSimpleDTO, FinancialIncomeRecordDTO, FinancialIncomeRecordCreateDTO, FinancialIncomeRecordUpdateDTO>, IFinancialIncomeRecordService
    {
        public FinancialIncomeRecordService(
            IFinancialIncomeRecordRepository repository,
            IMapper mapper,
            IValidator<FinancialIncomeRecordUpdateDTO> updateValidator,
            IValidator<FinancialIncomeRecordCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
