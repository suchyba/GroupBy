using Microsoft.AspNetCore.Identity;

namespace GroupBy.Design.Repositories
{
    public interface IUserTokenRepository<TKey> : IAsyncRepository<IdentityUserToken<TKey>> where TKey : IEquatable<TKey>
    {

    }
}
