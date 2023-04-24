namespace GroupBy.Design.Services
{
    public interface IAsyncService<SimpleDTO, FullDTO, CreateDTO, UpdateDTO>
    {
        public Task<FullDTO> CreateAsync(CreateDTO model);
        public Task<FullDTO> UpdateAsync(UpdateDTO model);
        public Task DeleteAsync(SimpleDTO model);
        public Task<IEnumerable<SimpleDTO>> GetAllAsync(bool includeLocal = false);
        public Task<FullDTO> GetAsync(SimpleDTO model);
    }
}
