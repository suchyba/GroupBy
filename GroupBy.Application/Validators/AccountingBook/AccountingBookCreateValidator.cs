using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.DTO.AccountingBook;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.AccountingBook
{
    public class AccountingBookCreateValidator : AbstractValidator<AccountingBookCreateDTO>
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
        private async Task<bool> IdUnique(AccountingBookCreateDTO book, CancellationToken token)
        {
            return await accountingBookRepository.IsIdUnique(book.BookId, book.BookOrderNumberId);
        }
    }
}
