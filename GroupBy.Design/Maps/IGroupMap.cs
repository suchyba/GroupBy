using GroupBy.Data.Models;
using GroupBy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Design.Maps
{
    public interface IGroupMap
    {
        public Group ViewModelToDomain(GroupViewModel model);
        public GroupViewModel DomainToViewModel(Group domain);
        public IEnumerable<GroupViewModel> DomainToViewModel(IEnumerable<Group> domain);
        public GroupViewModel Create(GroupViewModel model);
        public bool Update(GroupViewModel model);
        public bool Delete(GroupViewModel model);
        public IEnumerable<GroupViewModel> GetAll();
        public GroupViewModel Get(int id);
    }
}
