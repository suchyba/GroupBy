using FluentValidation;
using GroupBy.Design.DTO.FinancialCategory;

namespace GroupBy.Application.Validators.FinancialCategory
{
    public class FinancialCategoryCreateValidator : AbstractValidator<FinancialCategoryCreateDTO>
    {
        public FinancialCategoryCreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
