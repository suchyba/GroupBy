using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IApplicationUserRepository : IAsyncRepository<ApplicationUser>
    {
        public IEnumerable<ApplicationUser> GetAll();
        public Task<ApplicationUser> GetByNormalizedUserNameAsync(string normalizedUserName);
        public Task<ApplicationUser> GetByNormalizedEmailAsync(string normalizedEmail);
    }
}
