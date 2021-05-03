using GroupBy.Application.Design.Repositories;
using GroupBy.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public abstract class AsyncRepository<Entity> : IAsyncRepository<Entity> where Entity: class
    {
        protected readonly GroupByDbContext context;

        public AsyncRepository(GroupByDbContext context)
        {
            this.context = context;
        }
        public abstract Task DeleteAsync(Entity domain);
        public abstract Task<Entity> GetAsync(Entity domain);
        public abstract Task<Entity> UpdateAsync(Entity domain);
        public async Task<Entity> CreateAsync(Entity domain)
        {
            await context.Set<Entity>().AddAsync(domain);
            await context.SaveChangesAsync();
            return domain;

        }
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await context.Set<Entity>().ToListAsync();
        }
    }
}
