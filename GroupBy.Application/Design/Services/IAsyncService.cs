using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAsyncService<ViewModel>
    {
        public Task<ViewModel> CreateAsync(ViewModel domain);
        public Task<ViewModel> UpdateAsync(ViewModel domain);
        public Task DeleteAsync(Guid id);
        public Task<IEnumerable<ViewModel>> GetAllAsync();
        public Task<ViewModel> GetAsync(Guid id);
    }
}
