using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IVolunteerRepository : IAsyncRepository<Volunteer>
    {
        public Task<IEnumerable<Group>> GetGroupsAsync(Guid volunteerId, bool includeLocal = false);
        public Task<IEnumerable<Group>> GetOwnedGroupsAsync(Guid volunteerId, bool includeLocal = false);
        public Task<IEnumerable<Project>> GetOwnedProjectsAsync(Guid volunteerId, bool includeLocal = false);
        public Task<IEnumerable<RegistrationCode>> GetOwnedRegistrationCodesAsync(Guid volunteerId, bool includeLocal = false);
    }
}
