using Microsoft.AspNetCore.Identity;

namespace GroupBy.Design.Repositories
{
    public interface IRoleClaimRepository<TKey> : IAsyncRepository<IdentityRoleClaim<TKey>> where TKey : IEquatable<TKey>
    {
        public Task<IEnumerable<IdentityRoleClaim<TKey>>> GetByRoleIdAsync(TKey roleId);
    }
}
