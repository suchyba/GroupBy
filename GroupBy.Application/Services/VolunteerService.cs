using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Project;
using GroupBy.Application.DTO.RegistrationCode;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class VolunteerService : AsyncService<Volunteer, VolunteerSimpleDTO, VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>, IVolunteerService
    {
        public VolunteerService(IVolunteerRepository volunteerRepository, IMapper mapper,
            IValidator<VolunteerCreateDTO> createValidator,
            IValidator<VolunteerUpdateDTO> updateValidator) : base(volunteerRepository, mapper, updateValidator, createValidator)
        {

        }
        public async Task<IEnumerable<GroupSimpleDTO>> GetGroupsAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<GroupSimpleDTO>>(await (repository as IVolunteerRepository).GetGroupsAsync(volunteerId));
        }

        public async Task<IEnumerable<GroupSimpleDTO>> GetOwnedGroupsAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<GroupSimpleDTO>>(await (repository as IVolunteerRepository).GetOwnedGroupsAsync(volunteerId));
        }

        public async Task<IEnumerable<ProjectSimpleDTO>> GetOwnedProjectsAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<ProjectSimpleDTO>>(await (repository as IVolunteerRepository).GetOwnedProjectsAsync(volunteerId));
        }

        public async Task<IEnumerable<RegistrationCodeListDTO>> GetOwnedRegistrationCodesAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<RegistrationCodeListDTO>>(await (repository as IVolunteerRepository).GetOwnedRegistrationCodesAsync(volunteerId));
        }
    }
}
