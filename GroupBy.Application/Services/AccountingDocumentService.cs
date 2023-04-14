using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class AccountingDocumentService : AsyncService<AccountingDocument, AccountingDocumentSimpleDTO, AccountingDocumentDTO, AccountingDocumentCreateDTO, AccountingDocumentSimpleDTO>, IAccountingDocumentService
    {
        public AccountingDocumentService(
            IAccountingDocumentRepository repository,
            IMapper mapper, IValidator<AccountingDocumentSimpleDTO> updateValidator,
            IValidator<AccountingDocumentCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

    }
}
