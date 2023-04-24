using Microsoft.AspNetCore.Identity;

namespace GroupBy.Design.Repositories
{
    public interface IUserClaimRepository<TKey> : IAsyncRepository<IdentityUserClaim<TKey>> where TKey : IEquatable<TKey>
    {
        public Task<IEnumerable<IdentityUserClaim<TKey>>> GetByUserIdAsync(TKey userId);
        public Task<IEnumerable<TKey>> GetUserIdsForClaimAsync(string claimType, string claimValue);
    }
}
