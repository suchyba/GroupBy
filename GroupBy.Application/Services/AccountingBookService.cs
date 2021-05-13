using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels.AccountingBook;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class AccountingBookService : AsyncService<AccountingBook, AccountingBookViewModel, AccountingBookCreateViewModel, AccountingBookViewModel>, IAccountingBookService
    {
        public AccountingBookService(IAccountingBookRepository accountingBookRepository, IMapper mapper, 
            IValidator<AccountingBookViewModel> updateValidator, IValidator<AccountingBookCreateViewModel> createValidator)
            :base(accountingBookRepository, mapper, updateValidator, createValidator)
        {

        }        
    }
}
