using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.Document;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class DocumentService : AsyncService<Document, DocumentDTO, DocumentDTO, DocumentCreateDTO, DocumentUpdateDTO>, IDocumentService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IGroupRepository groupRepository;

        public DocumentService(
            IDocumentRepository repository,
            IProjectRepository projectRepository,
            IGroupRepository groupRepository,
            IMapper mapper,
            IValidator<DocumentUpdateDTO> updateValidator,
            IValidator<DocumentCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.projectRepository = projectRepository;
            this.groupRepository = groupRepository;
        }

        public override async Task<DocumentDTO> GetAsync(DocumentDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<DocumentDTO>(await repository.GetAsync(mapper.Map<Document>(model), includes: new string[] { "RelatedProject", "Groups" }));
            }
        }

        public override async Task<IEnumerable<DocumentDTO>> GetAllAsync(bool includeLocal = false)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<DocumentDTO>>(await repository.GetAllAsync(includes: new string[] { "RelatedProject", "Groups" }));
            }
        }

        protected override async Task<Document> CreateOperationAsync(DocumentCreateDTO model)
        {
            var entity = mapper.Map<Document>(model);
            // TODO add checking if the file pointed by FilePath exists
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                if (entity.RelatedProject != null)
                {
                    entity.RelatedProject = await projectRepository.GetAsync(entity.RelatedProject, includes: new string[] { "ParentGroup", "ProjectGroup" });
                    if (!entity.Groups.Any(g => g.Id == entity.RelatedProject.ParentGroup.Id)
                        || (entity.RelatedProject.ProjectGroup != null && !entity.Groups.Any(g => g.Id == entity.RelatedProject.ProjectGroup.Id)))
                    {
                        throw new BadRequestException("Document must be related with project's parent group or project's group.");
                    }
                }

                var tempGroups = new List<Group>();
                foreach (var group in entity.Groups)
                {
                    Group g = await groupRepository.GetAsync(group);

                    tempGroups.Add(g);
                }

                entity.Groups = tempGroups;

                var createdDocument = await repository.CreateAsync(entity);

                await uow.Commit();
                return createdDocument;
            }
        }

        protected override async Task<Document> UpdateOperationAsync(Document entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var document = await repository.GetAsync(entity, includes: new string[] { "RelatedProject", "Groups" });
                document.Name = entity.Name;
                document.FilePath = entity.FilePath;

                if (entity.RelatedProject != null)
                {
                    entity.RelatedProject = await projectRepository.GetAsync(entity.RelatedProject, includes: new string[] { "ParentGroup", "ProjectGroup" });
                    if (!document.Groups.Contains(entity.RelatedProject.ParentGroup)
                        || (entity.RelatedProject.ProjectGroup != null && !document.Groups.Contains(entity.RelatedProject.ProjectGroup)))
                    {
                        throw new BadRequestException("Document must be related with project's parent group or project's group.");
                    }
                }
                document.RelatedProject = entity.RelatedProject;

                var updatedDocument = await repository.UpdateAsync(document);
                await uow.Commit();
                return updatedDocument;
            }
        }
    }
}
