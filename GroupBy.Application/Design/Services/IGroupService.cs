using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IGroupService : IAsyncService<GroupViewModel, GroupViewModel>
    {
        public Task<IEnumerable<VolunteerViewModel>> GetVolunteersAsync(int groupId);
    }
}
