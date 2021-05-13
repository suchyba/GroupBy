using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels.Group;
using GroupBy.Application.ViewModels.Volunteer;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class GroupService : AsyncService<Group, GroupViewModel, GroupCreateViewModel, GroupUpdateViewModel>, IGroupService
    {

        public GroupService(IGroupRepository groupRepository, IMapper mapper,
            IValidator<GroupCreateViewModel> createValidator, 
            IValidator<GroupUpdateViewModel> updateValidator) : base(groupRepository, mapper, updateValidator, createValidator)
        {

        }

        public async Task AddMember(int groupId, int volunteerId)
        {
            await (repository as IGroupRepository).AddMamber(groupId, volunteerId);
        }

        public async Task<IEnumerable<VolunteerSimpleViewModel>> GetVolunteersAsync(int groupId)
        {
            return mapper.Map<IEnumerable<VolunteerSimpleViewModel>>(await (repository as IGroupRepository).GetVolunteersAsync(groupId));
        }
    }
}
