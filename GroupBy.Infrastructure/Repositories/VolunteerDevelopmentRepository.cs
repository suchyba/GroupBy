using GroupBy.Domain;
using GroupBy.Application.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupBy.Application.Exceptions;

namespace GroupBy.Data.Repositories
{
    public class VolunteerDevelopmentRepository : IVolunteerAsyncRepository
    {
        private static List<Volunteer> volunteers;
        public VolunteerDevelopmentRepository()
        {
            if(volunteers == null)
            {
                volunteers = new List<Volunteer>
                {
                    new Volunteer{Id = Guid.NewGuid(), FirstNames = "Wojtek", LastName = "Kowalski", Address = "ul. Majowa 3 07-410 Ostrołęka", BirthDate = DateTime.Now, Confirmed = true},
                    new Volunteer{Id = Guid.NewGuid(), FirstNames = "Bartek", LastName = "Kowalski", Address = "ul. Majowa 3 07-410 Ostrołęka", BirthDate = DateTime.Now, Confirmed = true},
                    new Volunteer{Id = Guid.NewGuid(), FirstNames = "Paweł", LastName = "Kowalski", Address = "ul. Majowa 3 07-410 Ostrołęka", BirthDate = DateTime.Now, Confirmed = true},
                };
            }
        }
        public async Task<Volunteer> CreateAsync(Volunteer domain)
        {
            volunteers.Add(domain);
            return domain;
        }

        public async Task DeleteAsync(Guid id)
        {
            var volunteer = volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer == null)
                throw new NotFoundException("Volunteer", id);
            volunteers.Remove(volunteer);
        }

        public async Task<Volunteer> GetAsync(Guid id)
        {
            return volunteers.FirstOrDefault(v => v.Id == id);
        }

        public async Task<IEnumerable<Volunteer>> GetAllAsync()
        {
            return volunteers;
        }

        public async Task<Volunteer> UpdateAsync(Volunteer domain)
        {
            var volunteer = volunteers.FirstOrDefault(v => v.Id == domain.Id);
            if (volunteer == null)
                throw new NotFoundException("Volunteer", domain.Id);

            volunteer.FirstNames = domain.FirstNames ?? volunteer.FirstNames;
            volunteer.LastName = domain.LastName ?? volunteer.LastName;
            volunteer.Address = domain.Address ?? volunteer.Address;
            volunteer.Confirmed = domain.Confirmed;
            return volunteer;
        }
    }
}
