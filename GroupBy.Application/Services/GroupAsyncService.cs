using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class GroupAsyncService : IGroupAsyncService
    {
        public Task<GroupViewModel> CreateAsync(GroupViewModel domain)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VolunteerViewModel>> GetVolunteersAsync(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public Task<GroupViewModel> UpdateAsync(GroupViewModel domain)
        {
            throw new NotImplementedException();
        }
    }
}
