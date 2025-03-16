using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.DTO.AccountingBook;
using GroupBy.Design.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.AccountingBook
{
    public class AccountingBookCreateValidator : AbstractValidator<AccountingBookCreateDTO>
    {
        private readonly IAccountingBookRepository accountingBookRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public AccountingBookCreateValidator(IAccountingBookRepository accountingBookRepository, IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.accountingBookRepository = accountingBookRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
            RuleFor(a => a.BookIdentificator)
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

            RuleFor(a => a.AccountingBookTemplateId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
        private async Task<bool> IdUnique(AccountingBookCreateDTO book, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return await accountingBookRepository.IsIdUnique(book.BookIdentificator, book.BookOrderNumberId);
            }
        }
    }
}
