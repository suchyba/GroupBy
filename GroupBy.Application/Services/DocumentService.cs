using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.Document;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;

namespace GroupBy.Application.Services
{
    public class DocumentService : AsyncService<Document, DocumentDTO, DocumentDTO, DocumentCreateDTO, DocumentUpdateDTO>, IDocumentService
    {
        public DocumentService(
            IDocumentRepository repository,
            IMapper mapper,
            IValidator<DocumentUpdateDTO> updateValidator,
            IValidator<DocumentCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }
    }
}
