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
    public class VolunteerAsyncService : IVolunteerAsyncService
    {
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IMapper mapper;

        public VolunteerAsyncService(IVolunteerRepository volunteerRepository, IMapper mapper)
        {
            this.volunteerRepository = volunteerRepository;
            this.mapper = mapper;
        }
        public async Task<VolunteerViewModel> CreateAsync(VolunteerViewModel model)
        {
            var validator = new VolunteerValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            return mapper.Map<VolunteerViewModel>(await volunteerRepository.CreateAsync(mapper.Map<Volunteer>(model)));
        }

        public async Task DeleteAsync(VolunteerViewModel model)
        {
            await volunteerRepository.DeleteAsync(mapper.Map<Volunteer>(model));
        }

        public async Task<VolunteerViewModel> GetAsync(VolunteerViewModel model)
        {
            return mapper.Map<VolunteerViewModel>(await volunteerRepository.GetAsync(mapper.Map<Volunteer>(model)));
        }

        public async Task<IEnumerable<VolunteerViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<VolunteerViewModel>>(await volunteerRepository.GetAllAsync());
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupsAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<GroupViewModel>>(await volunteerRepository.GetGroupsAsync(volunteerId));
        }

        public async Task<VolunteerViewModel> UpdateAsync(VolunteerViewModel model)
        {
            var validator = new VolunteerValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            return mapper.Map<VolunteerViewModel>(await volunteerRepository.UpdateAsync(mapper.Map<Volunteer>(model)));
        }
    }
}
