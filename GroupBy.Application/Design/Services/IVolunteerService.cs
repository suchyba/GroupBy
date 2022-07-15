using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Project;
using GroupBy.Application.DTO.RegistrationCode;
using GroupBy.Application.DTO.Volunteer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IVolunteerService : IAsyncService<VolunteerSimpleDTO, VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>
    {
        public Task<IEnumerable<GroupSimpleDTO>> GetGroupsAsync(int volunteerId);
        public Task<IEnumerable<GroupSimpleDTO>> GetOwnedGroupsAsync(int volunteerId);
        public Task<IEnumerable<ProjectSimpleDTO>> GetOwnedProjectsAsync(int volunteerId);
        public Task<IEnumerable<RegistrationCodeListDTO>> GetOwnedRegistrationCodesAsync(int volunteerId);
    }
}
