using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.FinancialOutcomeRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class FinancialOutcomeRecordService : AsyncService<FinancialOutcomeRecord, FinancialOutcomeRecordSimpleDTO, FinancialOutcomeRecordDTO, FinancialOutcomeRecordCreateDTO, FinancialOutcomeRecordUpdateDTO>, IFinancialOutcomeRecordService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IAccountingBookRepository accountingBookRepository;
        private readonly IAccountingDocumentRepository accountingDocumentRepository;

        public FinancialOutcomeRecordService(
            IFinancialOutcomeRecordRepository repository,
            IProjectRepository projectRepository,
            IAccountingBookRepository accountingBookRepository,
            IAccountingDocumentRepository accountingDocumentRepository,
            IMapper mapper,
            IValidator<FinancialOutcomeRecordUpdateDTO> updateValidator,
            IValidator<FinancialOutcomeRecordCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.projectRepository = projectRepository;
            this.accountingBookRepository = accountingBookRepository;
            this.accountingDocumentRepository = accountingDocumentRepository;
        }

        public override async Task<FinancialOutcomeRecordDTO> GetAsync(FinancialOutcomeRecordSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<FinancialOutcomeRecordDTO>(await repository.GetAsync(mapper.Map<FinancialOutcomeRecord>(model), includes: new string[] { "RelatedProject", "RelatedDocument", "Book" }));
            }
        }

        public override async Task DeleteAsync(FinancialOutcomeRecordSimpleDTO model)
        {
            var entity = mapper.Map<FinancialOutcomeRecord>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var accountingBook = await accountingBookRepository.GetAsync(entity);
                if (accountingBook.Locked)
                    throw new BadRequestException("Cannot remove record from locked book");

                await base.DeleteAsync(model);
            }
        }

        protected override async Task<FinancialOutcomeRecord> CreateOperationAsync(FinancialOutcomeRecord entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Book = await accountingBookRepository.GetAsync(entity.Book, false, includes: "RelatedGroup");

                if (entity.Book.Locked)
                    throw new BadRequestException("Cannot add record to locked book");

                entity.RelatedDocument = await accountingDocumentRepository.GetAsync(entity.RelatedDocument, false, includes: "Groups");
                if (!entity.RelatedDocument.Groups.Contains(entity.Book.RelatedGroup))
                    throw new BadRequestException("Document and accounting book must be related with the same group");

                if (entity.RelatedProject != null)
                {
                    entity.RelatedProject = await projectRepository.GetAsync(entity.RelatedProject, false, includes: new string[] { "ParentGroup", "ProjectGroup" });
                    if (entity.RelatedProject.ParentGroup != entity.Book.RelatedGroup
                        && entity.RelatedProject.ProjectGroup != entity.Book.RelatedGroup)
                        throw new BadRequestException("Project and accounting book must be related with the same group");
                }

                var createdRecord = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdRecord;
            }
        }
    }
}
