using GroupBy.Application.ViewModels.Group;
using GroupBy.Application.ViewModels.Volunteer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IGroupService : IAsyncService<GroupViewModel, GroupCreateViewModel, GroupUpdateViewModel>
    {
        public Task<IEnumerable<VolunteerSimpleViewModel>> GetVolunteersAsync(int groupId);
        public Task AddMember(int groupId, int volunteerId);
    }
}
