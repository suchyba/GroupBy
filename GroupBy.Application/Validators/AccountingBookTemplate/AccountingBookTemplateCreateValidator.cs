using FluentValidation;
using GroupBy.Design.DTO.AccountingBookTemplate;

namespace GroupBy.Application.Validators.AccountingBookTemplate
{
    public class AccountingBookTemplateCreateValidator : AbstractValidator<AccountingBookTemplateCreateDTO>
    {
        public AccountingBookTemplateCreateValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(t => t.Categories)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
