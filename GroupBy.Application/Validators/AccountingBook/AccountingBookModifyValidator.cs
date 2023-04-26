using FluentValidation;
using GroupBy.Design.DTO.AccountingBook;

namespace GroupBy.Application.Validators.AccountingBook
{
    public class AccountingBookUpdateValidator : AbstractValidator<AccountingBookSimpleDTO>
    {
        public AccountingBookUpdateValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(a => a.BookIdentificator)
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
