using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.FinancialRecord;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingBookService : AsyncService<AccountingBook, AccountingBookSimpleDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookSimpleDTO>, IAccountingBookService
    {
        public AccountingBookService(IAccountingBookRepository accountingBookRepository, IMapper mapper, 
            IValidator<AccountingBookSimpleDTO> updateValidator, IValidator<AccountingBookCreateDTO> createValidator)
            :base(accountingBookRepository, mapper, updateValidator, createValidator)
        {

        }

        public async Task<IEnumerable<FinancialRecordDTO>> GetFinancialRecordsAsync(AccountingBookSimpleDTO domain)
        {
            return mapper.Map<IEnumerable<FinancialRecordDTO>>(await (repository as IAccountingBookRepository).GetFinancialRecordsAsync(mapper.Map<AccountingBook>(domain)));
        }
    }
}
