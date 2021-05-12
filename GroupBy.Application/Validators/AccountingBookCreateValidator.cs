﻿using FluentValidation;
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
    public class AccountingBookCreateValidator : AbstractValidator<AccountingBookCreateViewModel>
    {
        private readonly IAccountingBookRepository accountingBookRepository;

        public AccountingBookCreateValidator(IAccountingBookRepository accountingBookRepository)
        {
            this.accountingBookRepository = accountingBookRepository;
            
            RuleFor(a => a.BookId)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .NotNull();

            RuleFor(a => a.BookOrderNumberId)
                .GreaterThan(0).WithMessage("{PropertyName} must be grater than 0.")
                .NotNull();

            RuleFor(a => a)
                .MustAsync(IdUnique).WithMessage("Combination of book id and order number must be unique.");

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(a => a.RelatedGroupId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
        private async Task<bool> IdUnique(AccountingBookCreateViewModel book, CancellationToken token)
        {
            return await accountingBookRepository.IsIdUnique(book.BookId, book.BookOrderNumberId);
        }
    }
}