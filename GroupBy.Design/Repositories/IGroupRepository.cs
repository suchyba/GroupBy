using GroupBy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Design.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        public IEnumerable<Volunteer> GetVolunteers(Group group);
    }
}
