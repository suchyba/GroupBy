using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid VolunteerId { get; set; }

        [Required]
        public virtual Volunteer RelatedVolunteer { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
    }

}
