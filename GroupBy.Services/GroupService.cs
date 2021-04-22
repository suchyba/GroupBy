using GroupBy.Data.Models;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository repository;

        public GroupService(IGroupRepository repository)
        {
            this.repository = repository;
        }
        public Group Create(Group group)
        {
            return repository.Create(group);
        }

        public bool Delete(Group group)
        {
            return repository.Delete(group.Id);
        }

        public Group Get(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<Group> GetAll()
        {
            return repository.GetAll();
        }

        public bool Update(Group group)
        {
            return repository.Update(group);
        }
    }
}
