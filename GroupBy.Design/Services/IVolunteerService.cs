using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Project;
using GroupBy.Design.TO.RegistrationCode;
using GroupBy.Design.TO.Volunteer;

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
