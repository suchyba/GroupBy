using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators
{
    public class AccountingBookValidator : AbstractValidator<AccountingBookViewModel>
    {
        private readonly IAccountingBookRepository accountingBookRepository;

        public AccountingBookValidator(IAccountingBookRepository accountingBookRepository)
        {
            RuleFor(a => a.BookId)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .MustAsync(AccountingNumberUnique).WithMessage("{PropertyName} must be unique.")
                .NotNull();

            RuleFor(a => a.BookOrderNumberId)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .NotNull();

            RuleFor(a => a)
                .MustAsync(AccountingOrderNumberUnique).WithMessage("Accounting book order number must be unique.").OverridePropertyName("BookOrderNumberId");

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            this.accountingBookRepository = accountingBookRepository;
        }

        private async Task<bool> AccountingNumberUnique(int number, CancellationToken token)
        {
            return !(await accountingBookRepository.IsAccountingBookNumberUnique(number));
        }
        private async Task<bool> AccountingOrderNumberUnique(AccountingBookViewModel book, CancellationToken token)
        {
            return !(await accountingBookRepository.IsAccountingBookOrderNumberUnique(book.BookId, book.BookOrderNumberId));
        }
    }
}
