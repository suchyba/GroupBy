using GroupBy.Design.DTO.Group;
using GroupBy.Design.DTO.Project;
using GroupBy.Design.DTO.RegistrationCode;
using GroupBy.Design.DTO.Volunteer;

namespace GroupBy.Design.Services
{
    public interface IVolunteerService : IAsyncService<VolunteerSimpleDTO, VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>
    {
        public Task<IEnumerable<GroupSimpleDTO>> GetGroupsAsync(Guid volunteerId);
        public Task<IEnumerable<GroupSimpleDTO>> GetOwnedGroupsAsync(Guid volunteerId);
        public Task<IEnumerable<ProjectSimpleDTO>> GetOwnedProjectsAsync(Guid volunteerId);
        public Task<IEnumerable<RegistrationCodeListDTO>> GetOwnedRegistrationCodesAsync(Guid volunteerId);
    }
}
