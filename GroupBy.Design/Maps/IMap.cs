using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Design.Maps
{
    public interface IMap<EntityViewModel, EntityDomain>
    {
        public EntityDomain ViewModelToDomain(EntityViewModel model);
        public EntityViewModel DomainToViewModel(EntityDomain domain);
        public IEnumerable<EntityViewModel> DomainToViewModel(IEnumerable<EntityDomain> domain);
        public EntityViewModel Create(EntityViewModel model);
        public bool Update(EntityViewModel model);
        public bool Delete(EntityViewModel model);
        public IEnumerable<EntityViewModel> GetAll();
        public EntityViewModel Get(EntityViewModel model);
    }
}
