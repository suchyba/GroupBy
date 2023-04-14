namespace GroupBy.Design.Repositories
{
    public interface IAsyncRepository<Entity>
    {
        public Task<Entity> CreateAsync(Entity domain);
        public Task<Entity> UpdateAsync(Entity domain);
        public Task DeleteAsync(Entity domain);
        public Task<IEnumerable<Entity>> GetAllAsync(bool includeLocal = false, params string[] includes);
        public Task<Entity> GetAsync(object domain, bool includeLocal = false, params string[] includes);
    }
}
