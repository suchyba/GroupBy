using FluentValidation;
using GroupBy.Application.ViewModels.AccountingBook;

namespace GroupBy.Application.Validators.AccountingBook
{
    public class AccountingBookUpdateValidator : AbstractValidator<AccountingBookViewModel>
    {
        public AccountingBookUpdateValidator()
        {
            RuleFor(a => a.BookId)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .NotNull();

            RuleFor(a => a.BookOrderNumberId)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .NotNull();

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
