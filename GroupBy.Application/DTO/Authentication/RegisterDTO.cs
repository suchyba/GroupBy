using GroupBy.Application.DTO.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Authentication
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RelatedVolunteerFirstNames { get; set; }
        public string RelatedVolunteerLastName { get; set; }
        public DateTime RelatedVolunteerBirthDate { get; set; }
        public string RelatedVolunteerPhoneNumber { get; set; }
        public string RelatedVolunteerAddress { get; set; }
        public string RegistrationCode { get; set; }
    }
}
