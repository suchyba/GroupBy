using FluentValidation;
using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Application.Validators.FinancialCategoryValue
{
    public class FinancialCategoryValueCreateValidator : AbstractValidator<FinancialCategoryValueCreateDTO>
    {
        public FinancialCategoryValueCreateValidator()
        {
            RuleFor(v => v.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(v => v.Value)
                .NotEqual(0).WithMessage("{PropertyName} must be other than 0");
        }
    }
}
