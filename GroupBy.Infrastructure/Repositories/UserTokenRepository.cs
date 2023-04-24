using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class UserTokenRepository : AsyncRepository<IdentityUserToken<Guid>>, IUserTokenRepository<Guid>
    {
        public UserTokenRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<IdentityUserToken<Guid>, bool>> CompareKeys(object entity)
        {
            return t => entity.GetType().GetProperty("UserId").GetValue(entity).Equals(t.UserId)
                && entity.GetType().GetProperty("LoginProvider").GetValue(entity).Equals(t.LoginProvider)
                && entity.GetType().GetProperty("Name").GetValue(entity).Equals(t.Name);
        }
    }
}
