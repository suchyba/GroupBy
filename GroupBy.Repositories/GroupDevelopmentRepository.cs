using GroupBy.Data.Models;
using GroupBy.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Repositories
{
    public class GroupDevelopmentRepository : IGroupRepository
    {
        public Group Create(Group group)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Group Get(int id)
        {
            return new Group { Id = id, Name = "Nowa", Description = "test" };
        }

        public IEnumerable<Group> GetAll()
        {
            return new List<Group> { 
                new Group { Id= 0, Name = "test", Description = "test"},
                new Group { Id= 1, Name = "test2", Description = "test2"}
            };
        }

        public bool Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
