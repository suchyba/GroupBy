using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IAsyncRepository<Entity>
    {
        public Task<Entity> CreateAsync(Entity domain);
        public Task<Entity> UpdateAsync(Entity domain);
        public Task DeleteAsync(Entity domain);
        public Task<IEnumerable<Entity>> GetAllAsync();
        public Task<Entity> GetAsync(Entity domain);
    }
}
