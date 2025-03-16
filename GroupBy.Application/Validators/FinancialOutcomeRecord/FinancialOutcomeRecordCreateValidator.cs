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
    public class FinancialOutcomeRecordCreateValidator : AbstractValidator<FinancialOutcomeRecordCreateDTO>
    {
        private readonly IFinancialCategoryRepository financialCategoryRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public FinancialOutcomeRecordCreateValidator(IFinancialCategoryRepository financialCategoryRepository, IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.financialCategoryRepository = financialCategoryRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;

            RuleFor(r => r.BookId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedDocumentId)
                .NotEmpty().WithMessage("{PropertyName} have to be greater then 0");
            RuleFor(r => r.RelatedProjectId)
                .NotEmpty().When(r => r.RelatedProjectId.HasValue).WithMessage("{PropertyName} have to be greater then 0");
            RuleFor(r => r.Values)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleForEach(r => r.Values)
                .SetValidator(new FinancialCategoryValueCreateValidator())
                .MustAsync(IsOutcome).WithMessage("Provided category is not an outcome");
        }

        private async Task<bool> IsOutcome(FinancialCategoryValueCreateDTO categoryValue, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
                return !(await financialCategoryRepository.GetAsync(new { Id = categoryValue.CategoryId })).Income;
        }
    }
}
