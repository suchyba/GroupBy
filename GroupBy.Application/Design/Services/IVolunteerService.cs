using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IVolunteerService : IAsyncService<VolunteerViewModel, VolunteerCreateViewModel, VolunteerUpdateViewModel>
    {
        public Task<IEnumerable<GroupViewModel>> GetGroupsAsync(int volunteerId);
    }
}
