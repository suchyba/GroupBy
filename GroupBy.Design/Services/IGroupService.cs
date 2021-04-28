using GroupBy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Design.Services
{
    public interface IGroupService : IService<Group>
    {
        public IEnumerable<Volunteer> GetVolunteers(Group domain);
    }
}
