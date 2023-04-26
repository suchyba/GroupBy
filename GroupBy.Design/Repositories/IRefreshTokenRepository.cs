using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>
    {
        public Task<RefreshToken> GetByTokenAsync(string token);
    }
}
