using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RefreshTokenRepository : AsyncRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContext context) : base(context)
        {

        }

        public async override Task<RefreshToken> GetAsync(RefreshToken domain)
        {
            var token = await context.Set<RefreshToken>()
                .FirstOrDefaultAsync(t => t.Id == domain.Id);

            if (token == null)
                throw new NotFoundException("RefreshToken", new RefreshToken { Id = domain.Id });

            return token;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            var refreshToken = await context.Set<RefreshToken>()
                .Include(t => t.Owner)
                .Include(t => t.ReplacedByToken)
                .FirstOrDefaultAsync(t => t.Token == token);

            if (refreshToken == null)
                throw new NotFoundException("RereshToken", new RefreshToken { Token = token });

            return refreshToken;
        }

        public async Task<ApplicationUser> GetOwner(string token)
        {
            var refreshToken = await GetByTokenAsync(token);

            return refreshToken.Owner;
        }

        public override async Task<RefreshToken> UpdateAsync(RefreshToken domain)
        {
            var token = await GetAsync(domain);
            token.ReplacedByToken = domain.ReplacedByToken;
            token.Revoked = domain.Revoked;
            token.RevokedByIP = domain.RevokedByIP;
            
            await context.SaveChangesAsync();
            return token;
        }
    }
}
