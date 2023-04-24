using Microsoft.AspNetCore.Identity;

namespace GroupBy.Design.Repositories
{
    public interface IUserLoginRepository<TKey> : IAsyncRepository<IdentityUserLogin<TKey>> where TKey : IEquatable<TKey>
    {
        public Task<IEnumerable<IdentityUserLogin<TKey>>> GetByUserIdAsync(TKey userId);
    }
}
