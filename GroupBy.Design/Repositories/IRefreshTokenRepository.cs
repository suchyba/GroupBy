using GroupBy.Domain.Entities;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>
    {
        public Task<RefreshToken> GetByTokenAsync(string token);
        public Task<ApplicationUser> GetOwner(string token);
    }
}
