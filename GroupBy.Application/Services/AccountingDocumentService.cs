using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.AccountingDocument;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class AccountingDocumentService : AsyncService<AccountingDocument, AccountingDocumentSimpleDTO, AccountingDocumentDTO, AccountingDocumentCreateDTO, AccountingDocumentSimpleDTO>, IAccountingDocumentService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IProjectRepository projectRepository;

        public AccountingDocumentService(
            IAccountingDocumentRepository repository,
            IGroupRepository groupRepository,
            IProjectRepository projectRepository,
            IMapper mapper, IValidator<AccountingDocumentSimpleDTO> updateValidator,
            IValidator<AccountingDocumentCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.groupRepository = groupRepository;
            this.projectRepository = projectRepository;
        }

        protected override async Task<AccountingDocument> CreateOperationAsync(AccountingDocumentCreateDTO model)
        {
            var entity = mapper.Map<AccountingDocument>(model);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var tempGroups = new List<Group>();
                foreach (var group in entity.Groups)
                {
                    tempGroups.Add(await groupRepository.GetAsync(group));
                }

                entity.Groups = tempGroups;

                if (entity.RelatedProject != null)
                    entity.RelatedProject = await projectRepository.GetAsync(entity.RelatedProject);

                var createdBook = await repository.CreateAsync(entity);

                await uow.Commit();
                return createdBook;
            }
        }
    }
}
