using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RoleRepository : AsyncRepository<IdentityRole<Guid>>, IRoleRepository<Guid>
    {
        public RoleRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IdentityRole<Guid>> GetByNameAsync(string roleName)
        {
            return await context.Set<IdentityRole<Guid>>().FirstOrDefaultAsync(r => r.Name == roleName);
        }

        protected override Expression<Func<IdentityRole<Guid>, bool>> CompareKeys(object entity)
        {
            return r => entity.GetType().GetProperty("Id").GetValue(entity).Equals(r.Id);
        }
    }
}
