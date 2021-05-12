using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators
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
