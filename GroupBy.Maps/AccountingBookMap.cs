using GroupBy.Data.Models;
using GroupBy.Design.Maps;
using GroupBy.Design.Services;
using GroupBy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Maps
{
    public class AccountingBookMap : IAccountingBookMap
    {
        private readonly IAccountingBookService accountingBookService;

        public AccountingBookMap(IAccountingBookService accountingBookService)
        {
            this.accountingBookService = accountingBookService;
        }
        public AccountingBookViewModel Create(AccountingBookViewModel model)
        {
            return DomainToViewModel(accountingBookService.Create(ViewModelToDomain(model)));
        }

        public bool Delete(AccountingBookViewModel model)
        {
            return accountingBookService.Delete(ViewModelToDomain(model));
        }


        public AccountingBookViewModel Get(AccountingBookViewModel model)
        {
            return DomainToViewModel(accountingBookService.Get(ViewModelToDomain(model)));
        }

        public IEnumerable<AccountingBookViewModel> GetAll()
        {
            return DomainToViewModel(accountingBookService.GetAll());
        }

        public bool Update(AccountingBookViewModel model)
        {
            return accountingBookService.Update(ViewModelToDomain(model));
        }
        public AccountingBookViewModel DomainToViewModel(AccountingBook domain)
        {
            return new AccountingBookViewModel
            {
                BookId = domain.BookId,
                BookOrderNumberId = domain.BookOrderNumberId,
                Locked = domain.Locked,
                Name = domain.Name
            };
        }

        public IEnumerable<AccountingBookViewModel> DomainToViewModel(IEnumerable<AccountingBook> domain)
        {
            return domain?.Select(ab => DomainToViewModel(ab));
        }

        public AccountingBook ViewModelToDomain(AccountingBookViewModel model)
        {
            return new AccountingBook
            {
                BookId = model.BookId,
                BookOrderNumberId = model.BookOrderNumberId,
                Locked = model.Locked,
                Name = model.Name
            };
        }
    }
}
