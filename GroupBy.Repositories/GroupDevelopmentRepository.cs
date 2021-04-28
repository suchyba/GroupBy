using GroupBy.Core.Entities;
using GroupBy.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Infrastructure.Repositories
{
    public class GroupDevelopmentRepository : IGroupRepository
    {
        private static List<Group> groups;
        public GroupDevelopmentRepository()
        {
            if (groups == null)
            {
                groups = new List<Group>
                {
                    new Group { Id= 0, Name = "test", Description = "test", Members = new List<Volunteer>
                    {
                        new Volunteer {Id = "0", FirstNames = "test", LastName = "last", Address = "a", Confirmed = true},
                        new Volunteer {Id = "1", FirstNames = "test1", LastName = "last1", Address = "a", Confirmed = true},
                        new Volunteer {Id = "2", FirstNames = "test2", LastName = "last2", Address = "a", Confirmed = true}
                    }},
                    new Group { Id= 1, Name = "test2", Description = "test2"}
                };
            }
        }
        public Group Create(Group group)
        {
            groups.Add(group);
            return group;
        }

        public bool Delete(Group domain)
        {
            try
            {
                groups.Remove(groups.FirstOrDefault(g => g.Id == domain.Id));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Group Get(Group domain)
        {
            return groups.FirstOrDefault(g => g.Id == domain.Id);
        }

        public IEnumerable<Group> GetAll()
        {
            return groups.OrderBy(g => g.Id);
        }

        public IEnumerable<Volunteer> GetVolunteers(Group group)
        {
            Group dbgroup = groups.FirstOrDefault(g => g.Id == group.Id);

            return dbgroup?.Members;
        }

        public bool Update(Group group)
        {
            var model = groups.FirstOrDefault(g => g.Id == group.Id);
            if (model == null)
                return false;

            model.Name = group.Name ?? model.Name;
            model.Description = group.Description ?? model.Description;
            return true;
        }
    }
}
