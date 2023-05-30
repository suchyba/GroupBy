using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.AccountingBook;
using GroupBy.Design.DTO.AccountingDocument;
using GroupBy.Design.DTO.Document;
using GroupBy.Design.DTO.Group;
using GroupBy.Design.DTO.Project;
using GroupBy.Design.DTO.Volunteer;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class GroupService : AsyncService<Group, GroupSimpleDTO, GroupDTO, GroupCreateDTO, GroupUpdateDTO>, IGroupService
    {
        private readonly IVolunteerRepository volunteerRepository;

        public GroupService(
            IGroupRepository groupRepository,
            IVolunteerRepository volunteerRepository,
            IMapper mapper,
            IValidator<GroupCreateDTO> createValidator,
            IValidator<GroupUpdateDTO> updateValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(groupRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.volunteerRepository = volunteerRepository;
        }

        public override async Task<GroupDTO> GetAsync(GroupSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<GroupDTO>(await repository.GetAsync(new { Id = mapper.Map<Group>(model).Id }, includes: new string[] { "Owner", "AccountingBooks", "ParentGroup", "ChildGroups", "Members", "ProjectsRealisedInGroup" }));
            }
        }

        protected override async Task<Group> CreateOperationAsync(Group entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Owner = await volunteerRepository.GetAsync(entity.Owner);

                if (entity.ParentGroup != null)
                    entity.ParentGroup = await repository.GetAsync(new { Id = entity.ParentGroup.Id });

                Group createdGroup = await repository.CreateAsync(entity);

                await AddMemberAsync(createdGroup.Id, createdGroup.Owner.Id);
                await uow.Commit();

                return createdGroup;
            }
        }

        protected override async Task<Group> UpdateOperationAsync(Group entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Owner = await volunteerRepository.GetAsync(new { Id = entity.Owner.Id });

                var updatedGroup = await repository.UpdateAsync(entity);
                await uow.Commit();

                return updatedGroup;
            }
        }

        public async Task AddMemberAsync(Guid groupId, Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var group = await repository.GetAsync(new { Id = groupId }, includeLocal: true, includes: new string[] { "Members", "RelatedProject" });
                var volunteer = await volunteerRepository.GetAsync(new { Id = volunteerId });

                if (group.Members?.Contains(volunteer) ?? false)
                    throw new BadRequestException($"Volunteer {volunteer.Id} is already a member of the group {group.Id}");

                if (group.RelatedProject != null && !group.RelatedProject.Active)
                    throw new BadRequestException("Cannot add member to the inactive project");

                await (repository as IGroupRepository).AddMemberAsync(groupId, volunteer, true);
                await uow.Commit();
            }
        }

        public async Task<IEnumerable<AccountingBookSimpleDTO>> GetAccountingBooksAsync(Guid groupId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<AccountingBookSimpleDTO>>((await repository.GetAsync(new { Id = groupId }, includeLocal: false, includes: "AccountingBooks")).AccountingBooks);
            }
        }

        public async Task<IEnumerable<AccountingDocumentSimpleDTO>> GetAccountingDocumentsAsync(Guid groupId, Guid? projectId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<AccountingDocumentSimpleDTO>>((await (repository as IGroupRepository).GetAccountingDocumentsAsync(groupId, projectId)));
            }
        }

        public async Task<IEnumerable<DocumentDTO>> GetDocumentsAsync(Guid groupId, Guid? projectId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<DocumentDTO>>((await (repository as IGroupRepository).GetDocumentsAsync(groupId, projectId)));
            }
        }

        public async Task<IEnumerable<ProjectSimpleDTO>> GetProjectsAsync(Guid groupId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<ProjectSimpleDTO>>((await (repository as IGroupRepository).GetProjectsAsync(groupId)));
            }
        }

        public async Task<IEnumerable<GroupSimpleDTO>> GetSubgroupsAsync(Guid groupId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<GroupSimpleDTO>>(await (repository as IGroupRepository).GetSubgroupsAsync(groupId));
            }
        }

        public async Task<IEnumerable<VolunteerSimpleDTO>> GetVolunteersAsync(Guid groupId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<VolunteerSimpleDTO>>(await (repository as IGroupRepository).GetVolunteersAsync(groupId));
            }
        }

        public async Task RemoveMemberAsync(Guid groupId, Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var volunteer = await volunteerRepository.GetAsync(new { Id = volunteerId });
                if (!await (repository as IGroupRepository).IsMember(groupId, volunteer))
                    throw new BadRequestException("Volunteer is not a member of the group");

                var group = await repository.GetAsync(new { Id = groupId }, includeLocal: false, includes: "Owner");
                if (group.Owner == volunteer)
                    throw new BadRequestException("Volunteer cannot be an owner of the group");

                await (repository as IGroupRepository).RemoveMemberAsync(groupId, volunteer);
                await uow.Commit();
            }
        }
    }
}
