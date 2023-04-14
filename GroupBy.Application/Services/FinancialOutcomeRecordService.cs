using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.FinancialOutcomeRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class FinancialOutcomeRecordService : AsyncService<FinancialOutcomeRecord, FinancialOutcomeRecordSimpleDTO, FinancialOutcomeRecordDTO, FinancialOutcomeRecordCreateDTO, FinancialOutcomeRecordUpdateDTO>, IFinancialOutcomeRecordService
    {
        public FinancialOutcomeRecordService(
            IFinancialOutcomeRecordRepository repository,
            IMapper mapper,
            IValidator<FinancialOutcomeRecordUpdateDTO> updateValidator,
            IValidator<FinancialOutcomeRecordCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
