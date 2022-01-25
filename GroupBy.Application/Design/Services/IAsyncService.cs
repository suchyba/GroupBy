using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAsyncService<SimpleDTO, FullDTO, CreateDTO, UpdateDTO>
    {
        public Task<FullDTO> CreateAsync(CreateDTO model);
        public Task<FullDTO> UpdateAsync(UpdateDTO model);
        public Task DeleteAsync(SimpleDTO model);
        public Task<IEnumerable<SimpleDTO>> GetAllAsync();
        public Task<FullDTO> GetAsync(SimpleDTO model);
    }
}
