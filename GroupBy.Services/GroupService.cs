using GroupBy.Core.Entities;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Infrastructure.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository repository;

        public GroupService(IGroupRepository repository)
        {
            this.repository = repository;
        }
        public Group Create(Group domain)
        {
            return repository.Create(domain);
        }

        public bool Delete(Group domain)
        {
            return repository.Delete(domain);
        }

        public Group Get(Group domain)
        {
            return repository.Get(domain);
        }

        public IEnumerable<Group> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<Volunteer> GetVolunteers(Group domain)
        {
            return repository.GetVolunteers(domain);
        }

        public bool Update(Group group)
        {
            return repository.Update(group);
        }
    }
}
