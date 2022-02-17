using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int VolunteerId { get; set; }
        [Required]
        public virtual Volunteer RelatedVolunteer { get; set; }
    }
}
