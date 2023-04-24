using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Invitation to group
    /// </summary>
    public class InvitationToGroup
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Groups from this invitation came from <seealso cref="Group"/>
        /// </summary>
        public virtual Group TargetGroup { get; set; }
        /// <summary>
        /// The inviter <seealso cref="Volunteer"/>
        /// </summary>
        public virtual Volunteer Inviter { get; set; }
        /// <summary>
        /// The invited volunteer <seealso cref="Volunteer"/>
        /// </summary>
        public virtual Volunteer Invited { get; set; }
    }
}
