using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class VolunteerAsyncService : IVolunteerAsyncService
    {
        public Task<VolunteerViewModel> CreateAsync(VolunteerViewModel domain)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<VolunteerViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VolunteerViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupViewModel>> GetGroupsAsync(Guid volunteerId)
        {
            throw new NotImplementedException();
        }

        public Task<VolunteerViewModel> UpdateAsync(VolunteerViewModel domain)
        {
            throw new NotImplementedException();
        }
    }
}
