using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// The volunteer
    /// </summary>
    public class Volunteer
    {
        /// <summary>
        /// The names of the volunteer
        /// </summary>
        [Required]
        public string FirstNames { get; set; }
        /// <summary>
        /// The last name of the volunteer
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// The identification number of the volunteer
        /// </summary>
        [Key]
        public string Id { get; set; }
        public Position GetPosition(Group group)
        {
            // nie takie proste
            return PositionHistory.Where(h => h.DismissDate == null && h.RelatedGroup == group).FirstOrDefault()?.Position;
        }
        /// <summary>
        /// Information about positions of this volunteer <seealso cref="Position"/>
        /// </summary>
        public virtual IEnumerable<PositionRecord> PositionHistory { get; set; }
        /// <summary>
        /// Birth date of this volunteer
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// The phone number of the volunteer
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The address of the volunteer
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The list of projects where this volunteer is the owner <seealso cref="Project"/>
        /// </summary>
        public  virtual IEnumerable<Project> OwnedProjects { get; set; }
        /// <summary>
        /// The groups of which this volunteer is member
        /// </summary>
        public  virtual IEnumerable<Group> Groups { get; set; }
        /// <summary>
        /// Describes if this volunteer has been confirmed
        /// </summary>
        public bool Confirmed { get; set; }
        /// <summary>
        /// The rank of this volunteer
        /// </summary>
        public virtual Rank Rank { get; set; }
        public virtual IEnumerable<RegistrationCode> RegistrationCodes { get; set; }
        [ForeignKey("Invited")]
        public virtual IEnumerable<InvitationToGroup> YourInvitations { get; set; }
        [ForeignKey("Invited")]
        public virtual IEnumerable<InvitationToGroup> InvitationsToGroup { get; set; }
    }
}
