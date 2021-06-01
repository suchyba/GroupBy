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
    public class VolunteerService : AsyncService<Volunteer, VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>, IVolunteerService
    {
        public VolunteerService(IVolunteerRepository volunteerRepository, IMapper mapper,
            IValidator<VolunteerCreateDTO> createValidator,
            IValidator<VolunteerUpdateDTO> updateValidator) : base(volunteerRepository, mapper, updateValidator, createValidator)
        {

        }
        public async Task<IEnumerable<GroupDTO>> GetGroupsAsync(int volunteerId)
        {
            return mapper.Map<IEnumerable<GroupDTO>>(await (repository as IVolunteerRepository).GetGroupsAsync(volunteerId));
        }
    }
}
