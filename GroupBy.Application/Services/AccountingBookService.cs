using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class AccountingBookService : AsyncService<AccountingBook, AccountingBookDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookDTO>, IAccountingBookService
    {
        public AccountingBookService(IAccountingBookRepository accountingBookRepository, IMapper mapper, 
            IValidator<AccountingBookDTO> updateValidator, IValidator<AccountingBookCreateDTO> createValidator)
            :base(accountingBookRepository, mapper, updateValidator, createValidator)
        {

        }        
    }
}
