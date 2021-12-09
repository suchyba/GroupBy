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
    public class AccountingDocumentService : AsyncService<AccountingDocument, AccountingDocumentDTO, AccountingDocumentCreateDTO, AccountingDocumentDTO>, IAccountingDocumentService
    {
        public AccountingDocumentService(
            IAccountingDocumentRepository repository, 
            IMapper mapper, IValidator<AccountingDocumentDTO> updateValidator, 
            IValidator<AccountingDocumentCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }

    }
}
