using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RoleClaimRepository : AsyncRepository<IdentityRoleClaim<Guid>>, IRoleClaimRepository<Guid>
    {
        public RoleClaimRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<IdentityRoleClaim<Guid>>> GetByRoleIdAsync(Guid roleId)
        {
            return await context.Set<IdentityRoleClaim<Guid>>().Where(r => r.RoleId == roleId).ToListAsync();
        }

        protected override Expression<Func<IdentityRoleClaim<Guid>, bool>> CompareKeys(object entity)
        {
            return c => entity.GetType().GetProperty("Id").GetValue(entity).Equals(c.Id);
        }
    }
}
