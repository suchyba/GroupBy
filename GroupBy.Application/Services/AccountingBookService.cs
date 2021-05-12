using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.Validators;
using GroupBy.Application.ViewModels;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
