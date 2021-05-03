﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// The volunteer
    /// </summary>
    public class Volunteer
    {
        /// <summary>
        /// The identification number of the volunteer
        /// </summary>
        [Key]
        public int Id { get; set; }
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
        /// Information about positions of this volunteer <seealso cref="Position"/>
        /// </summary>
        public virtual IEnumerable<PositionRecord> PositionHistory { get; set; }
        /// <summary>
        /// Birth date of this volunteer
        /// </summary>
        [Required]
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
        [InverseProperty("Owner")]
        public virtual IEnumerable<Project> OwnedProjects { get; set; }
        /// <summary>
        /// The groups of which this volunteer is member
        /// </summary>
        [InverseProperty("Members")]
        public virtual IEnumerable<Group> Groups { get; set; }
        /// <summary>
        /// The groups of which this volunteer is owner
        /// </summary>
        [InverseProperty("Owner")]
        public virtual IEnumerable<Group> OwnedGroups { get; set; }
        /// <summary>
        /// Describes if this volunteer has been confirmed
        /// </summary>
        public bool Confirmed { get; set; }
        /// <summary>
        /// The rank of this volunteer
        /// </summary>
        public virtual Rank Rank { get; set; }
        /// <summary>
        /// Registration codes generated by this volunteer <seealso cref="RegistrationCode"/>
        /// </summary>
        public virtual IEnumerable<RegistrationCode> RegistrationCodes { get; set; }
        /// <summary>
        /// Invitations to groups where this volunteer is an inviter <seealso cref="InvitationToGroup"/>
        /// </summary>
        [InverseProperty("Inviter")]
        public virtual IEnumerable<InvitationToGroup> YourInvitations { get; set; }
        /// <summary>
        /// Invitations to groups where this volunteer is an invited one <seealso cref="InvitationToGroup"/>
        /// </summary>
        [InverseProperty("Invited")]
        public virtual IEnumerable<InvitationToGroup> InvitationsToGroup { get; set; }
        /// <summary>
        /// Tasks assigned to this volunteer <seealso cref="TODOListElement"/>
        /// </summary>
        public virtual IEnumerable<TODOListElement> AssignedTasks { get; set; }
        /// <summary>
        /// Agreement accepted by this volunteer <seealso cref="Agreement"/>
        /// </summary>
        public virtual IEnumerable<Agreement> Agreements { get; set; }
        public virtual IdentityModel Identity { get; set; }
    }
}
