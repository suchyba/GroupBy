using GroupBy.Core.Entities;
using GroupBy.Design.Maps;
using GroupBy.Design.Services;
using GroupBy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Maps
{
    public class GroupMap : IGroupMap
    {
        private readonly IGroupService service;

        public GroupMap(IGroupService service)
        {
            this.service = service;
        }
        public GroupViewModel Create(GroupViewModel model)
        {
            return DomainToViewModel(service.Create(ViewModelToDomain(model)));
        }

        public bool Delete(GroupViewModel model)
        {
            return service.Delete(ViewModelToDomain(model));
        }

        public GroupViewModel DomainToViewModel(Group domain)
        {
            if (domain == null)
                return null;

            return new GroupViewModel { Id = domain.Id, Name = domain.Name, Description = domain.Description };
        }

        public IEnumerable<GroupViewModel> DomainToViewModel(IEnumerable<Group> domain)
        {
            return domain.Select(g => DomainToViewModel(g));
        }

        public GroupViewModel Get(GroupViewModel model)
        {
            return DomainToViewModel(service.Get(ViewModelToDomain(model)));
        }

        public IEnumerable<GroupViewModel> GetAll()
        {
            return DomainToViewModel(service.GetAll());
        }

        public bool Update(GroupViewModel model)
        {
            return service.Update(ViewModelToDomain(model));
        }

        public Group ViewModelToDomain(GroupViewModel model)
        {
            return new Group { Id = model.Id, Name = model.Name, Description = model.Description };
        }
    }
}
