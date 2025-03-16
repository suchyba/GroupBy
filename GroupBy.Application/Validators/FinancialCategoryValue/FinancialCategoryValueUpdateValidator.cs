using FluentValidation;
using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Application.Validators.FinancialCategoryValue
{
    public class FinancialCategoryValueUpdateValidator : AbstractValidator<FinancialCategoryValueUpdateDTO>
    {
        public FinancialCategoryValueUpdateValidator()
        {
            RuleFor(v => v.Value)
                .NotEqual(0).WithMessage("{PropertyName} must be other than 0");
        }
    }
}
