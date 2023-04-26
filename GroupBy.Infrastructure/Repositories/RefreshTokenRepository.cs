using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RefreshTokenRepository : AsyncRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            var refreshToken = await context.Set<RefreshToken>()
                .FirstOrDefaultAsync(t => t.Token == token);

            if (refreshToken == null)
                throw new NotFoundException("RereshToken", new RefreshToken { Token = token });

            return refreshToken;
        }

        protected override Expression<Func<RefreshToken, bool>> CompareKeys(object entity)
        {
            return t => entity.GetType().GetProperty("Id").GetValue(entity).Equals(t.Id);
        }
    }
}
