using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public abstract class AsyncRepository<Entity> : IAsyncRepository<Entity> where Entity : class
    {
        protected GroupByDbContext context => dBcontextLocator.GetDbContext();

        private readonly IDbContextLocator<GroupByDbContext> dBcontextLocator;

        public AsyncRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator)
        {
            this.dBcontextLocator = dBcontextLocator;
        }
        public virtual async Task DeleteAsync(Entity domain)
        {
            var entity = await GetAsync(domain);

            context.Set<Entity>().Remove(entity);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DeleteNotPermittedException(typeof(Entity).Name);
            }

        }
        public async Task<Entity> GetAsync(object domain, bool includeLocal = false, params string[] includes)
        {
            Entity entity = null;
            if (includeLocal)
            {
                await LoadToLocalAsync(includes);
                entity = context.Set<Entity>().Local.FirstOrDefault(e => CompareKeys(e, domain));
            }
            else
                entity = await includes.Aggregate(context.Set<Entity>().AsTracking(), (current, include) => current.Include(include)).FirstOrDefaultAsync(e => CompareKeys(e, domain));

            if (entity == null)
                throw new NotFoundException(typeof(Entity).Name, domain);

            return entity;

        }
        public abstract Task<Entity> UpdateAsync(Entity domain);
        public virtual async Task<Entity> CreateAsync(Entity domain)
        {
            await context.Set<Entity>().AddAsync(domain);
            return domain;
        }
        public virtual async Task<IEnumerable<Entity>> GetAllAsync(bool includeLocal = false, params string[] includes)
        {
            if (includeLocal)
            {
                await LoadToLocalAsync(includes);
                return context.Set<Entity>().Local;
            }
            else
                return await includes.Aggregate(context.Set<Entity>().AsTracking(), (current, include) => current.Include(include)).ToListAsync();
        }
        protected async Task LoadToLocalAsync(params string[] includes)
        {
            await includes.Aggregate(context.Set<Entity>().AsTracking(), (current, include) => current.Include(include)).LoadAsync();
        }
        protected virtual bool CompareKeys(Entity entity, object keys)
        {
            var entityKeys = entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length > 0);
            var keysProperties = keys.GetType().GetProperties();
            foreach (var key in entityKeys)
            {
                var keyProperty = keysProperties.FirstOrDefault(p => p.Name == key.Name);
                if (keyProperty == null)
                    return false;
                if (!key.GetValue(entity).Equals(keyProperty.GetValue(keys)))
                    return false;
            }
            return true;
        }
    }
}
