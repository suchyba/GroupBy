using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.Validators;
using GroupBy.Application.ViewModels;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<SimpleVolunteerViewModel>> GetVolunteersAsync(int groupId)
        {
            return mapper.Map<IEnumerable<SimpleVolunteerViewModel>>(await (repository as IGroupRepository).GetVolunteersAsync(groupId));
        }
    }
}
