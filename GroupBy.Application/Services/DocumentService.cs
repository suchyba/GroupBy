using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Document;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class DocumentService : AsyncService<Document, DocumentDTO, DocumentCreateDTO, DocumentUpdateDTO>, IDocumentService
    {
        public DocumentService(IDocumentRepository repository, IMapper mapper, IValidator<DocumentUpdateDTO> updateValidator, IValidator<DocumentCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
    }
}
