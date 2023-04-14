using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.FinancialRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingBookService : AsyncService<AccountingBook, AccountingBookSimpleDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookSimpleDTO>, IAccountingBookService
    {
        public AccountingBookService(
            IAccountingBookRepository accountingBookRepository,
            IMapper mapper,
            IValidator<AccountingBookSimpleDTO> updateValidator,
            IValidator<AccountingBookCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(accountingBookRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        public async Task<IEnumerable<FinancialRecordSimpleDTO>> GetFinancialRecordsAsync(AccountingBookSimpleDTO domain)
        {
            return mapper.Map<IEnumerable<FinancialRecordSimpleDTO>>(await (repository as IAccountingBookRepository).GetFinancialRecordsAsync(mapper.Map<AccountingBook>(domain)));
        }
    }
}
