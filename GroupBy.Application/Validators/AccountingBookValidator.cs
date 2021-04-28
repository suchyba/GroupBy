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
        private readonly IAccountingBookAsyncRepository accountingBookRepository;

        public AccountingBookValidator(IAccountingBookAsyncRepository accountingBookRepository)
        {
            RuleFor(a => a.BookNumber)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .MustAsync(AccountingNumberUnique).WithMessage("{PropertyName} must be unique.")
                .NotNull();

            RuleFor(a => a.BookOrderNumber)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .MustAsync(AccountingOrderNumberUnique).WithMessage("{PropertyName} must be unique.")
                .NotNull();

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            this.accountingBookRepository = accountingBookRepository;
        }

        private async Task<bool> AccountingNumberUnique(int number, CancellationToken token)
        {
            return !(await accountingBookRepository.IsAccountingBookNumberUnique(number));
        }
        private async Task<bool> AccountingOrderNumberUnique(int number, CancellationToken token)
        {
            return !(await accountingBookRepository.IsAccountingBookOrderNumberUnique(number));
        }
    }
}
