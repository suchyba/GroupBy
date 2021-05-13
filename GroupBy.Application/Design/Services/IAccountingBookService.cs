using GroupBy.Application.ViewModels.AccountingBook;

namespace GroupBy.Application.Design.Services
{
    public interface IAccountingBookService : IAsyncService<AccountingBookViewModel, AccountingBookCreateViewModel, AccountingBookViewModel>
    {

    }
}
