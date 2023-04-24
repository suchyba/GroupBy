using Microsoft.AspNetCore.Identity;

namespace GroupBy.Design.Repositories
{
    public interface IUserRoleRepository<TKey> : IAsyncRepository<IdentityUserRole<TKey>> where TKey : IEquatable<TKey>
    {
        public Task<IEnumerable<TKey>> GetUserIdsByRoleIdAsync(TKey roleIdName);
        public Task<IEnumerable<TKey>> GetRoleIdsByUserIdAsync(TKey userId);
    }
}
