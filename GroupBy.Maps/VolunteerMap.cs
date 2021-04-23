using GroupBy.Data.Models;
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
    public class VolunteerMap : IVolunteerMap
    {
        private readonly IVolunteerService service;

        public VolunteerMap(IVolunteerService service)
        {
            this.service = service;
        }
        public VolunteerViewModel Create(VolunteerViewModel model)
        {
            return DomainToViewModel(service.Create(ViewModelToDomain(model)));
        }

        public bool Delete(VolunteerViewModel model)
        {
            return service.Delete(ViewModelToDomain(model));
        }

        public VolunteerViewModel DomainToViewModel(Volunteer domain)
        {
            return new VolunteerViewModel
            {
                Id = domain.Id,
                FirstNames = domain.FirstNames,
                LastName = domain.LastName,
                Address = domain.Address,
                Confirmed = domain.Confirmed,
                BirthDate = domain.BirthDate,
                PhoneNumber = domain.PhoneNumber
            };
        }

        public IEnumerable<VolunteerViewModel> DomainToViewModel(IEnumerable<Volunteer> domain)
        {
            return domain?.Select(v => DomainToViewModel(v));
        }

        public VolunteerViewModel Get(VolunteerViewModel model)
        {
            return DomainToViewModel(service.Get(ViewModelToDomain(model)));
        }

        public IEnumerable<VolunteerViewModel> GetAll()
        {
            return DomainToViewModel(service.GetAll());
        }

        public bool Update(VolunteerViewModel model)
        {
            return service.Update(ViewModelToDomain(model));
        }

        public Volunteer ViewModelToDomain(VolunteerViewModel model)
        {
            return new Volunteer
            {
                Id = model.Id,
                FirstNames = model.FirstNames,
                LastName = model.LastName,
                Address = model.Address,
                Confirmed = model.Confirmed,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate
            };
        }
    }
}
