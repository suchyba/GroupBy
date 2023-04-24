using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ApplicationUserRepository : AsyncRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.Set<ApplicationUser>();
        }

        public async Task<ApplicationUser> GetByNormalizedEmailAsync(string normalizedEmail)
        {
            var user = await context.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
            if (user == null)
                throw new NotFoundException(typeof(ApplicationUser).Name, new { NormalizedEmail = normalizedEmail });

            return user;
        }

        public async Task<ApplicationUser> GetByNormalizedUserNameAsync(string normalizedUserName)
        {
            var user = await context.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
            if (user == null)
                throw new NotFoundException(typeof(ApplicationUser).Name, new { NormalizedUserName = normalizedUserName });

            return user;
        }

        protected override Expression<Func<ApplicationUser, bool>> CompareKeys(object entity)
        {
            return u => entity.GetType().GetProperty("Id").GetValue(entity).Equals(u.Id);
        }
    }
}
