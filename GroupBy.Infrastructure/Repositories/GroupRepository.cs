using GroupBy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using GroupBy.Application.Design.Repositories;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class GroupRepository : IGroupAsyncRepository
    {
        public Task<Group> CreateAsync(Group domain)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<Group> UpdateAsync(Group domain)
        {
            throw new NotImplementedException();
        }
    }
}
