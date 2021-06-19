using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAsyncService<DTO, CreateDTO, UpdateDTO>
    {
        public Task<DTO> CreateAsync(CreateDTO model);
        public Task<DTO> UpdateAsync(UpdateDTO model);
        public Task DeleteAsync(DTO model);
        public Task<IEnumerable<DTO>> GetAllAsync();
        public Task<DTO> GetAsync(DTO model);
    }
}
