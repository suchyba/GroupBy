using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class GroupService : AsyncService<Group, GroupDTO, GroupCreateDTO, GroupUpdateDTO>, IGroupService
    {

        public GroupService(IGroupRepository groupRepository, IMapper mapper,
            IValidator<GroupCreateDTO> createValidator, 
            IValidator<GroupUpdateDTO> updateValidator) : base(groupRepository, mapper, updateValidator, createValidator)
        {

        }

        public async Task AddMember(int groupId, int volunteerId)
        {
            await (repository as IGroupRepository).AddMamber(groupId, volunteerId);
        }

        public async Task<IEnumerable<VolunteerSimpleDTO>> GetVolunteersAsync(int groupId)
        {
            return mapper.Map<IEnumerable<VolunteerSimpleDTO>>(await (repository as IGroupRepository).GetVolunteersAsync(groupId));
        }
    }
}
