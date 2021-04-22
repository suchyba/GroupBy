using GroupBy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Design.Repositories
{
    public interface IGroupRepository
    {
        public Group Create(Group group);
        public bool Update(Group group);
        public bool Delete(int id);
        public IEnumerable<Group> GetAll();
        public Group Get(int id);
    }
}
