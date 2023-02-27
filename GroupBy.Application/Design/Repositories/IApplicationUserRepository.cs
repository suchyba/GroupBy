using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IApplicationUserRepository : IAsyncRepository<ApplicationUser>
    {
        public Task<List<RefreshToken>> GetRefreshTokensAsync(string email);
        public Task AddRefreshTokenAsync(string email, RefreshToken refreshToken);
        public Task RemoveOldRefreshTokens(string email, int TTL);
    }
}
