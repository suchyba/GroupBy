using Microsoft.AspNetCore.Identity;

namespace GroupBy.Design.Repositories
{
    public interface IRoleRepository<KeyType> : IAsyncRepository<IdentityRole<KeyType>> where KeyType : IEquatable<KeyType>
    {
        public Task<IdentityRole<KeyType>> GetByNameAsync(string roleName);
    }
}
