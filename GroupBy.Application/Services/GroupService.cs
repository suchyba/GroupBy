using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Project;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Domain.Entities;
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
            IValidator<GroupUpdateDTO> updateValidator)
            : base(groupRepository, mapper, updateValidator, createValidator)
        {

        }

        public async Task AddMemberAsync(int groupId, int volunteerId)
        {
            await (repository as IGroupRepository).AddMemberAsync(groupId, volunteerId);
        }

        public async Task<IEnumerable<AccountingBookSimpleDTO>> GetAccountingBooksAsync(int groupId)
        {
            return mapper.Map<IEnumerable<AccountingBookSimpleDTO>>((await (repository as IGroupRepository).GetAsync(new Group { Id = groupId })).AccountingBooks);
        }

        public async Task<IEnumerable<AccountingDocumentSimpleDTO>> GetAccountingDocumentsAsync(int groupId)
        {
            return mapper.Map<IEnumerable<AccountingDocumentSimpleDTO>>((await(repository as IGroupRepository).GetAccountingDocumentsAsync(groupId)));
        }

        public async Task<IEnumerable<ProjectSimpleDTO>> GetProjectsAsync(int groupId)
        {
            return mapper.Map<IEnumerable<ProjectSimpleDTO>>((await (repository as IGroupRepository).GetProjectsAsync(groupId)));
        }

        public async Task<IEnumerable<GroupSimpleDTO>> GetSubgroupsAsync(int groupId)
        {
            return mapper.Map<IEnumerable<GroupSimpleDTO>>(await (repository as IGroupRepository).GetSubgroupsAsync(groupId));
        }

        public async Task<IEnumerable<VolunteerSimpleDTO>> GetVolunteersAsync(int groupId)
        {
            return mapper.Map<IEnumerable<VolunteerSimpleDTO>>(await (repository as IGroupRepository).GetVolunteersAsync(groupId));
        }

        public async Task RemoveMemberAsync(int groupId, int volunteerId)
        {
            await (repository as IGroupRepository).RemoveMemberAsync(groupId, volunteerId);
        }
    }
}
