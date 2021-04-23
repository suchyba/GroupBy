using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Design.Services
{
    public interface IService <Entity>
    {
        public Entity Create(Entity domain);
        public bool Update(Entity domain);
        public bool Delete(Entity domain);
        public IEnumerable<Entity> GetAll();
        public Entity Get(Entity domain);
    }
}
