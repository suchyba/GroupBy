using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid VolunteerId { get; set; }

        [Required]
        public virtual Volunteer RelatedVolunteer { get; set; }
    }

}
