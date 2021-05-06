using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAsyncService<ViewModel, CreateViewModel>
    {
        public Task<ViewModel> CreateAsync(CreateViewModel model);
        public Task<ViewModel> UpdateAsync(ViewModel model);
        public Task DeleteAsync(ViewModel model);
        public Task<IEnumerable<ViewModel>> GetAllAsync();
        public Task<ViewModel> GetAsync(ViewModel model);
    }
}
