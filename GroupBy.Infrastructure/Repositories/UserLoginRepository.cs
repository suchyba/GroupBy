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
    public class UserLoginRepository : AsyncRepository<IdentityUserLogin<Guid>>, IUserLoginRepository<Guid>
    {
        public UserLoginRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<IdentityUserLogin<Guid>>> GetByUserIdAsync(Guid userId)
        {
            return await context.Set<IdentityUserLogin<Guid>>().Where(l => l.UserId == userId).ToListAsync();
        }

        protected override Expression<Func<IdentityUserLogin<Guid>, bool>> CompareKeys(object entity)
        {
            return l => entity.GetType().GetProperty("LoginProvider").GetValue(entity).Equals(l.LoginProvider)
                && entity.GetType().GetProperty("ProviderKey").GetValue(entity).Equals(l.ProviderKey);
        }
    }
}
