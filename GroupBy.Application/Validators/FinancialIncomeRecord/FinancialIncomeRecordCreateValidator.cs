using FluentValidation;
using GroupBy.Application.Validators.FinancialCategoryValue;
using GroupBy.Data.DbContexts;
using GroupBy.Design.DTO.FinancialCategoryValue;
using GroupBy.Design.DTO.FinancialIncomeRecord;
using GroupBy.Design.Repositories;
using GroupBy.Design.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.FinancialIncomeRecord
{
    public class FinancialIncomeRecordCreateValidator : AbstractValidator<FinancialIncomeRecordCreateDTO>
    {
        private readonly IFinancialCategoryRepository financialCategoryRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public FinancialIncomeRecordCreateValidator(IFinancialCategoryRepository financialCategoryRepository, IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.financialCategoryRepository = financialCategoryRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;

            RuleFor(r => r.BookId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedDocumentId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedProjectId)
                .NotEmpty()
                .When(r => r.RelatedProjectId.HasValue)
                .WithMessage("{PropertyName} cannot be empty");
            RuleFor(r => r.Values)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleForEach(r => r.Values)
                .SetValidator(new FinancialCategoryValueCreateValidator())
                .MustAsync(IsIncome).WithMessage("Provided category is not an income");
        }
        private async Task<bool> IsIncome(FinancialCategoryValueCreateDTO categoryValue, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
                return (await financialCategoryRepository.GetAsync(new {Id = categoryValue.CategoryId })).Income;
        }
    }
}
