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
    public class UserRoleRepository : AsyncRepository<IdentityUserRole<Guid>>, IUserRoleRepository<Guid>
    {
        public UserRoleRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<Guid>> GetRoleIdsByUserIdAsync(Guid userId)
        {
            return await context.Set<IdentityUserRole<Guid>>().Where(r => r.UserId == userId).Select(r => r.RoleId).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetUserIdsByRoleIdAsync(Guid roleId)
        {
            return await context.Set<IdentityUserRole<Guid>>().Where(r => r.RoleId == roleId).Select(r => r.UserId).ToListAsync();
        }

        protected override Expression<Func<IdentityUserRole<Guid>, bool>> CompareKeys(object entity)
        {
            return r => entity.GetType().GetProperty("UserId").GetValue(entity).Equals(r.UserId)
                && entity.GetType().GetProperty("RoleId").GetValue(entity).Equals(r.RoleId);
        }
    }
}
