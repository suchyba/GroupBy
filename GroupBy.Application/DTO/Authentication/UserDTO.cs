using GroupBy.Application.DTO.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Authentication
{
    public class UserDTO
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public VolunteerDTO RelatedVolunteer { get; set; }
    }
}
