using FluentValidation;
using GroupBy.Design.DTO.AccountingBookTemplate;

namespace GroupBy.Application.Validators.AccountingBookTemplate
{
    public class AccountingBookTemplateUpdateValidator : AbstractValidator<AccountingBookTemplateUpdateDTO>
    {
        public AccountingBookTemplateUpdateValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(t => t.Categories)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
