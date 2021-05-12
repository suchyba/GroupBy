using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IGroupRepository : IAsyncRepository<Group>
    {
        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(int group);
        public Task AddMamber(int groupId, int volunteerId);
    }
}
