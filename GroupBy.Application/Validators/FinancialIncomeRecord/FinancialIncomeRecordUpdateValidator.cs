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
    public class FinancialIncomeRecordUpdateValidator : AbstractValidator<FinancialIncomeRecordUpdateDTO>
    {
        private readonly IFinancialCategoryRepository financialCategoryRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public FinancialIncomeRecordUpdateValidator(IFinancialCategoryRepository financialCategoryRepository, IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.financialCategoryRepository = financialCategoryRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;

            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.Values)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleForEach(r => r.Values)
                .SetValidator(new FinancialCategoryValueUpdateValidator())
                .MustAsync(IsIncome).WithMessage("Provided category is not an income");
        }
        private async Task<bool> IsIncome(FinancialCategoryValueUpdateDTO categoryValue, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
                return (await financialCategoryRepository.GetAsync(new { Id = categoryValue.CategoryId })).Income;
        }
    }
}
