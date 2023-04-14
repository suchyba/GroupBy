using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.Document;
using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Project;
using GroupBy.Design.TO.Volunteer;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class GroupService : AsyncService<Group, GroupSimpleDTO, GroupDTO, GroupCreateDTO, GroupUpdateDTO>, IGroupService
    {

        public GroupService(
            IGroupRepository groupRepository,
            IMapper mapper,
            IValidator<GroupCreateDTO> createValidator,
            IValidator<GroupUpdateDTO> updateValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(groupRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        public override async Task<GroupDTO> CreateAsync(GroupCreateDTO model)
        {
            var validationResult = await createValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                Group createdGroup = await repository.CreateAsync(mapper.Map<Group>(model));

                await (repository as IGroupRepository).AddMemberAsync(createdGroup.Id, createdGroup.Owner.Id);
                await uow.Commit();

                return mapper.Map<GroupDTO>(createdGroup);
            }
        }

        public async Task AddMemberAsync(Guid groupId, Guid volunteerId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                await (repository as IGroupRepository).AddMemberAsync(groupId, volunteerId);
                await uow.Commit();
            }
        }

        public async Task<IEnumerable<AccountingBookSimpleDTO>> GetAccountingBooksAsync(Guid groupId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<AccountingBookSimpleDTO>>((await (repository as IGroupRepository).GetAsync(new Group { Id = groupId })).AccountingBooks);
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
                await (repository as IGroupRepository).RemoveMemberAsync(groupId, volunteerId);
                await uow.Commit();
            }
        }
    }
}
