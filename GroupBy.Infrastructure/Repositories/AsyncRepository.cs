using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public abstract class AsyncRepository<Entity> : IAsyncRepository<Entity> where Entity : class
    {
        protected readonly DbContext context;

        public AsyncRepository(DbContext context)
        {
            this.context = context;
        }
        public virtual async Task DeleteAsync(Entity domain)
        {
            var entity = await GetAsync(domain);

            context.Set<Entity>().Remove(entity);

            try
            {
                await context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new DeleteNotPermittedException(typeof(Entity).Name);
            }

        }
        public abstract Task<Entity> GetAsync(Entity domain);
        public abstract Task<Entity> UpdateAsync(Entity domain);
        public virtual async Task<Entity> CreateAsync(Entity domain)
        {
            await context.Set<Entity>().AddAsync(domain);
            await context.SaveChangesAsync();
            return domain;
        }
        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await context.Set<Entity>().ToListAsync();
        }
    }
}
