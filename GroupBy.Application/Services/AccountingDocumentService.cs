using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingDocumentService : AsyncService<AccountingDocument, AccountingDocumentSimpleDTO, AccountingDocumentDTO, AccountingDocumentCreateDTO, AccountingDocumentSimpleDTO>, IAccountingDocumentService
    {
        public AccountingDocumentService(
            IAccountingDocumentRepository repository, 
            IMapper mapper, IValidator<AccountingDocumentSimpleDTO> updateValidator, 
            IValidator<AccountingDocumentCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }

    }
}
