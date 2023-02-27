using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ApplicationUserRepository : AsyncRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DbContext context) : base(context)
        {

        }

        public async Task AddRefreshTokenAsync(string email, RefreshToken refreshToken)
        {
            var user = await GetAsync(new ApplicationUser() { Email = email });

            user.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
        }

        public override Task<ApplicationUser> GetAsync(ApplicationUser domain)
        {
            var user = context.Set<ApplicationUser>()
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email== domain.Email);

            if (user == null) 
                throw new NotFoundException("ApplicationUser", new { Email = domain.Email });

            return user;
        }

        public async Task<List<RefreshToken>> GetRefreshTokensAsync(string email)
        {
            var user = await context.Set<ApplicationUser>().Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == email);

            return user?.RefreshTokens;
        }

        public async Task RemoveOldRefreshTokens(string email, int TTL)
        {
            var user = await GetAsync(new ApplicationUser { Email = email });

            user.RefreshTokens.RemoveAll(t => !t.IsActive && t.Created.AddDays(TTL) <= DateTime.UtcNow);

            await context.SaveChangesAsync();
        }

        public override Task<ApplicationUser> UpdateAsync(ApplicationUser domain)
        {
            throw new NotImplementedException();
        }
    }
}
