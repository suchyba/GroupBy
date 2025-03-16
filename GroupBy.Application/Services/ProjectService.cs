using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.AccountingDocument;
using GroupBy.Design.DTO.FinancialRecord;
using GroupBy.Design.DTO.Project;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class ProjectService : AsyncService<Project, ProjectSimpleDTO, ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>, IProjectService
    {
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IGroupRepository groupRepository;

        public ProjectService(
            IProjectRepository repository,
            IVolunteerRepository volunteerRepository,
            IGroupRepository groupRepository,
            IMapper mapper,
            IValidator<ProjectUpdateDTO> updateValidator,
            IValidator<ProjectCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.volunteerRepository = volunteerRepository;
            this.groupRepository = groupRepository;
        }

        public override async Task<ProjectDTO> GetAsync(ProjectSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<ProjectDTO>(await repository.GetAsync(mapper.Map<Project>(model), includes: new string[] { "ParentGroup", "ProjectGroup", "Owner" }));
            }
        }

        protected override async Task<Project> CreateOperationAsync(ProjectCreateDTO model)
        {
            var entity = mapper.Map<Project>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Owner = await volunteerRepository.GetAsync(entity.Owner);
                entity.ParentGroup = await groupRepository.GetAsync(entity.ParentGroup);

                if (entity.Independent)
                {
                    var projectGroup = await groupRepository.CreateAsync(new Group
                    {
                        Description = $"This is a group of {entity.Name} project",
                        Name = entity.Name + " group",
                        ParentGroup = entity.ParentGroup,
                        Owner = entity.Owner
                    });

                    entity.ProjectGroup = projectGroup;

                    await groupRepository.AddMemberAsync(projectGroup.Id, entity.Owner, true);
                }
                else
                    entity.ProjectGroup = null;

                var createdProject = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdProject;
            }
        }

        protected override async Task<Project> UpdateOperationAsync(Project entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Owner = await volunteerRepository.GetAsync(entity.Owner);
                entity.ParentGroup = await groupRepository.GetAsync(entity.ParentGroup);
                if (entity.ProjectGroup != null)
                    entity.ProjectGroup = await groupRepository.GetAsync(entity.ProjectGroup);

                var updatedProject = await repository.UpdateAsync(entity);
                await uow.Commit();
                return updatedProject;
            }
        }

        public async Task<IEnumerable<AccountingDocumentSimpleDTO>> GetRelatedAccountingDocumentsAsync(ProjectSimpleDTO project)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<AccountingDocumentSimpleDTO>>((await (repository as IProjectRepository).GetRelatedAccountingDocumentsAsync(mapper.Map<Project>(project))));
            }
        }

        public async Task<IEnumerable<FinancialRecordSimpleDTO>> GetRelatedFinancialRecordsAsync(ProjectSimpleDTO project)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<FinancialRecordSimpleDTO>>((await repository.GetAsync(mapper.Map<Project>(project), includes: "RelatedFinnancialRecords.Values")).RelatedFinnancialRecords);
            }
        }
    }
}
