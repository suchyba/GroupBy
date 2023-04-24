using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            await Task.Run(() => context.Set<Entity>().Remove(domain));
        }
        public async Task<Entity> GetAsync(object domain, bool includeLocal = false, bool asTracking = true, params string[] includes)
        {
            Entity entity = null;
            if (includeLocal)
            {
                await LoadToLocalAsync(asTracking, includes);
                entity = context.Set<Entity>().Local.FirstOrDefault(CompareKeys(domain).Compile());
            }
            else
            {
                IQueryable<Entity> set = null;
                if (asTracking)
                    set = context.Set<Entity>().AsTracking();
                else
                    set = context.Set<Entity>().AsNoTracking();

                //entity = (await includes.Aggregate(set, (current, include) => current.Include(include)).ToListAsync()).Find(e => CompareKeys(e, domain));
                entity = await includes.Aggregate(set, (current, include) => current.Include(include)).FirstOrDefaultAsync(CompareKeys(domain));
            }

            if (entity == null)
                throw new NotFoundException(typeof(Entity).Name, domain);

            return entity;

        }
        public virtual async Task<Entity> UpdateAsync(Entity domain)
        {
            return await Task.Run(() => context.Set<Entity>().Update(domain).Entity);
        }
        public virtual async Task<Entity> CreateAsync(Entity domain)
        {
            return (await context.Set<Entity>().AddAsync(domain)).Entity;
        }
        public virtual async Task<IEnumerable<Entity>> GetAllAsync(bool includeLocal = false, params string[] includes)
        {
            if (includeLocal)
            {
                await LoadToLocalAsync(true, includes);
                return context.Set<Entity>().Local;
            }
            else
                return await includes.Aggregate(context.Set<Entity>().AsTracking(), (current, include) => current.Include(include)).ToListAsync();
        }
        protected async Task LoadToLocalAsync(bool asTracking = true, params string[] includes)
        {
            IQueryable<Entity> set = null;
            if (asTracking)
                set = context.Set<Entity>().AsTracking();
            else
                set = context.Set<Entity>().AsNoTracking();

            await includes.Aggregate(set, (current, include) => current.Include(include)).LoadAsync();
        }
        protected abstract Expression<Func<Entity, bool>> CompareKeys(object entity);
    }
}
