using GroupBy.Application.ViewModels.Group;
using GroupBy.Application.ViewModels.Volunteer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IVolunteerService : IAsyncService<VolunteerViewModel, VolunteerCreateViewModel, VolunteerUpdateViewModel>
    {
        public Task<IEnumerable<GroupViewModel>> GetGroupsAsync(int volunteerId);
    }
}
