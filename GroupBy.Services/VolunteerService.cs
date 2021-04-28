using GroupBy.Core.Entities;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Infrastructure.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository repository;

        public VolunteerService(IVolunteerRepository repository)
        {
            this.repository = repository;
        }
        public Volunteer Create(Volunteer domain)
        {
            return repository.Create(domain);
        }

        public bool Delete(Volunteer domain)
        {
            return repository.Delete(domain);
        }

        public Volunteer Get(Volunteer domain)
        {
            return repository.Get(domain);
        }

        public IEnumerable<Volunteer> GetAll()
        {
            return repository.GetAll();
        }

        public bool Update(Volunteer domain)
        {
            return repository.Update(domain);
        }
    }
}
