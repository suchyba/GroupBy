using AutoMapper;
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
    public class GroupAsyncService : IGroupAsyncService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public GroupAsyncService(IGroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }
        public async Task<GroupViewModel> CreateAsync(GroupViewModel domain)
        {
            var validator = new GroupValidator();
            var validationResult = await validator.ValidateAsync(domain);
            if(!validationResult.IsValid)
                throw (new ValidationException(validationResult));

            return mapper.Map<GroupViewModel>(await groupRepository.CreateAsync(mapper.Map<Group>(domain)));
        }

        public async Task DeleteAsync(GroupViewModel model)
        {
            await groupRepository.DeleteAsync(mapper.Map<Group>(model));
        }

        public async Task<GroupViewModel> GetAsync(GroupViewModel model)
        {
            return mapper.Map<GroupViewModel>(await groupRepository.GetAsync(mapper.Map<Group>(model)));
        }

        public async Task<IEnumerable<GroupViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<GroupViewModel>>(await groupRepository.GetAllAsync());
        }

        public async Task<IEnumerable<VolunteerViewModel>> GetVolunteersAsync(int groupId)
        {
            return mapper.Map<IEnumerable<VolunteerViewModel>>(await groupRepository.GetVolunteersAsync(groupId));
        }

        public async Task<GroupViewModel> UpdateAsync(GroupViewModel domain)
        {
            var validator = new GroupValidator();
            var validationResult = await validator.ValidateAsync(domain);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            return mapper.Map<GroupViewModel>(await groupRepository.UpdateAsync(mapper.Map<Group>(domain)));
        }
    }
}
