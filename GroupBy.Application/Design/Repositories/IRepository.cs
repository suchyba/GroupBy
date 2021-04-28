using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IRepository <Entity>
    {
        public Entity Create(Entity domain);
        public bool Update(Entity domain);
        public bool Delete(Guid id);
        public IEnumerable<Entity> GetAll();
        public Entity Get(Guid id);
    }
}
