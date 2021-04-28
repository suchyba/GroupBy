using GroupBy.Domain;
using GroupBy.Application.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupBy.Application.Exceptions;

namespace GroupBy.Data.Repositories
{
    public class GroupDevelopmentRepository : IGroupAsyncRepository
    {
        private static List<Group> groups;
        public GroupDevelopmentRepository()
        {
            if (groups == null)
            {
                groups = new List<Group>
                {
                    new Group { Id= Guid.NewGuid(), Name = "test", Description = "test", Members = new List<Volunteer>
                    {
                        new Volunteer {Id = Guid.NewGuid(), FirstNames = "test", LastName = "last", Address = "a", Confirmed = true},
                        new Volunteer {Id = Guid.NewGuid(), FirstNames = "test1", LastName = "last1", Address = "a", Confirmed = true},
                        new Volunteer {Id = Guid.NewGuid(), FirstNames = "test2", LastName = "last2", Address = "a", Confirmed = true}
                    }},
                    new Group { Id= Guid.NewGuid(), Name = "test2", Description = "test2"}
                };
            }
        }
        public async Task<Group> CreateAsync(Group group)
        {
            groups.Add(group);
            return group;
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                groups.Remove(groups.FirstOrDefault(g => g.Id == id));
            }
            catch (Exception)
            {
                throw new NotFoundException("Group", id);
            }
        }
        public async Task<Group> GetAsync(Guid id)
        {
            return groups.FirstOrDefault(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return groups.OrderBy(g => g.Name);
        }

        public async Task<IEnumerable<Volunteer>> GetVolunteersAsync(Group group)
        {
            Group dbgroup = groups.FirstOrDefault(g => g.Id == group.Id);

            return dbgroup?.Members;
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            var model = groups.FirstOrDefault(g => g.Id == group.Id);
            if (model == null)
                throw new NotFoundException("Group", group.Id);

            model.Name = group.Name ?? model.Name;
            model.Description = group.Description ?? model.Description;
            return model;
        }
    }
}
