using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ApplicationUserRepository : AsyncRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.Set<ApplicationUser>();
        }

        public async Task AddRefreshTokenAsync(string email, RefreshToken refreshToken)
        {
            // TODO add uow
            var user = await GetAsync(new ApplicationUser() { Email = email });

            user.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetByNormalizedEmailAsync(string normalizedEmail)
        {
            var user = await context.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
            if (user == null)
                throw new NotFoundException(typeof(ApplicationUser).Name, new { NormalizedEmail = normalizedEmail });

            return user;
        }

        public override Task<ApplicationUser> GetAsync(ApplicationUser domain)
        {
            // TODO add uow
            var user = context.Set<ApplicationUser>()
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == domain.Email);

            if (user == null)
                throw new NotFoundException("ApplicationUser", new { Email = domain.Email });

            return user;
        }

        public async Task<ApplicationUser> GetByNormalizedUserNameAsync(string normalizedUserName)
        {
            var user = await context.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
            if (user == null)
                throw new NotFoundException(typeof(ApplicationUser).Name, new { NormalizedUserName = normalizedUserName });

            return user;
        }

        public async Task RemoveOldRefreshTokens(string email, int TTL)
        {
            // TODO add uow
            var user = await GetAsync(new ApplicationUser { Email = email });

            user.RefreshTokens.RemoveAll(t => !t.IsActive && t.Created.AddDays(TTL) <= DateTime.UtcNow);

            await context.SaveChangesAsync();
        }

        public async Task<List<RefreshToken>> GetRefreshTokensAsync(string email)
        {
            // TODO add uow
            var user = await context.Set<ApplicationUser>().Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == email);

            return user?.RefreshTokens;
        }

        protected override Expression<Func<ApplicationUser, bool>> CompareKeys(object entity)
        {
            return u => entity.GetType().GetProperty("Id").GetValue(entity).Equals(u.Id);
        }

        public override Task<ApplicationUser> UpdateAsync(ApplicationUser domain)
        {
            throw new NotImplementedException();
        }
    }
}
