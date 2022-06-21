using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Volunteer
{
    public class VolunteerSimpleDTO
    {
        public int Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
