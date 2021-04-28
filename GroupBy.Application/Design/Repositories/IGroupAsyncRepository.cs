using GroupBy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IGroupAsyncRepository : IAsyncRepository<Group>
    {
        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(Group group);
    }
}
