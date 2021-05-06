using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using GroupBy.Application.Design.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GroupBy.Data.Repositories
{
    public class GroupRepository : AsyncRepository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext context) : base(context)
        {

        }
        public override Task DeleteAsync(Group domain)
        {
            throw new NotImplementedException();
        }

        public override Task<Group> GetAsync(Group domain)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(int group)
        {
            throw new NotImplementedException();
        }

        public override Task<Group> UpdateAsync(Group domain)
        {
            throw new NotImplementedException();
        }
    }
}
