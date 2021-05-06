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
    public class GroupService : AsyncService<Group, GroupViewModel, GroupViewModel>, IGroupService
    {

        public GroupService(IGroupRepository groupRepository, IMapper mapper,
            IValidator<GroupViewModel> validator) : base(groupRepository, mapper, validator, validator)
        {

        }

        public Task<IEnumerable<VolunteerViewModel>> GetVolunteersAsync(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
