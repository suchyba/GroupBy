using GroupBy.Core.Entities;
using GroupBy.Design.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Infrastructure.Repositories
{
    public class VolunteerDevelopmentRepository : IVolunteerRepository
    {
        private static List<Volunteer> volunteers;
        public VolunteerDevelopmentRepository()
        {
            if(volunteers == null)
            {
                volunteers = new List<Volunteer>
                {
                    new Volunteer{Id = "00000000", FirstNames = "Wojtek", LastName = "Kowalski", Address = "ul. Majowa 3 07-410 Ostrołęka", BirthDate = DateTime.Now, Confirmed = true},
                    new Volunteer{Id = "11111111", FirstNames = "Bartek", LastName = "Kowalski", Address = "ul. Majowa 3 07-410 Ostrołęka", BirthDate = DateTime.Now, Confirmed = true},
                    new Volunteer{Id = "11111111", FirstNames = "Paweł", LastName = "Kowalski", Address = "ul. Majowa 3 07-410 Ostrołęka", BirthDate = DateTime.Now, Confirmed = true},
                };
            }
        }
        public Volunteer Create(Volunteer domain)
        {
            volunteers.Add(domain);
            return domain;
        }

        public bool Delete(Volunteer domain)
        {
            var volunteer = volunteers.FirstOrDefault(v => v.Id == domain.Id);
            if (volunteer == null)
                return false;

            volunteers.Remove(volunteer);
            return true;
        }

        public Volunteer Get(Volunteer domain)
        {
            return volunteers.FirstOrDefault(v => v.Id == domain.Id);
        }

        public IEnumerable<Volunteer> GetAll()
        {
            return volunteers;
        }

        public bool Update(Volunteer domain)
        {
            var volunteer = volunteers.FirstOrDefault(v => v.Id == domain.Id);
            if (volunteer == null)
                return false;

            volunteer.FirstNames = domain.FirstNames ?? volunteer.FirstNames;
            volunteer.LastName = domain.LastName ?? volunteer.LastName;
            volunteer.Address = domain.Address ?? volunteer.Address;
            volunteer.Confirmed = domain.Confirmed;
            return true;
        }
    }
}
