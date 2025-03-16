using FluentValidation;
using GroupBy.Application.Validators.FinancialCategoryValue;
using GroupBy.Data.DbContexts;
using GroupBy.Design.DTO.FinancialCategoryValue;
using GroupBy.Design.DTO.FinancialOutcomeRecord;
using GroupBy.Design.Repositories;
using GroupBy.Design.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.FinancialOutcomeRecord
{
    public class FinancialOutcomeRecordUpdateValidator : AbstractValidator<FinancialOutcomeRecordUpdateDTO>
    {
        private readonly IFinancialCategoryRepository financialCategoryRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public FinancialOutcomeRecordUpdateValidator(IFinancialCategoryRepository financialCategoryRepository, IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.financialCategoryRepository = financialCategoryRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
            
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.Values)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleForEach(r => r.Values)
                .SetValidator(new FinancialCategoryValueUpdateValidator())
                .MustAsync(IsOutcome).WithMessage("Provided category is not an outcome");
        }

        private async Task<bool> IsOutcome(FinancialCategoryValueUpdateDTO categoryValue, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
                return !(await financialCategoryRepository.GetAsync(new { Id = categoryValue.CategoryId })).Income;
        }
    }
}
