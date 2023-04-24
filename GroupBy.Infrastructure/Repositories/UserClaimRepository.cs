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
    public class UserClaimRepository : AsyncRepository<IdentityUserClaim<Guid>>, IUserClaimRepository<Guid>
    {
        public UserClaimRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<IdentityUserClaim<Guid>>> GetByUserIdAsync(Guid userId)
        {
            return await context.Set<IdentityUserClaim<Guid>>().Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetUserIdsForClaimAsync(string claimType, string claimValue)
        {
            return await context.Set<IdentityUserClaim<Guid>>().Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue).Select(c => c.UserId).ToListAsync();
        }

        protected override Expression<Func<IdentityUserClaim<Guid>, bool>> CompareKeys(object entity)
        {
            return c => entity.GetType().GetProperty("Id").GetValue(entity).Equals(c.Id);
        }
    }
}
