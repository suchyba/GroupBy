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
    public class VolunteerService : AsyncService<Volunteer, VolunteerViewModel, VolunteerCreateViewModel, VolunteerUpdateViewModel>, IVolunteerService
    {
        public VolunteerService(IVolunteerRepository volunteerRepository, IMapper mapper,
            IValidator<VolunteerCreateViewModel> createValidator,
            IValidator<VolunteerUpdateViewModel> updateValidator) : base(volunteerRepository, mapper, updateValidator, createValidator)
        {

        }
        public async Task<IEnumerable<GroupViewModel>> GetGroupsAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<GroupViewModel>>(await (repository as IVolunteerRepository).GetGroupsAsync(volunteerId));
        }
    }
}
