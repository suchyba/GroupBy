using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IVolunteerRepository : IAsyncRepository<Volunteer>
    {
        public Task<IEnumerable<Group>> GetGroupsAsync(int volunteerId);
        public Task<IEnumerable<Group>> GetOwnedGroupsAsync(int volunteerId);
        public Task<IEnumerable<Project>> GetOwnedProjectsAsync(int volunteerId);
        public Task<IEnumerable<RegistrationCode>> GetOwnedRegistrationCodesAsync(int volunteerId);

    }
}
