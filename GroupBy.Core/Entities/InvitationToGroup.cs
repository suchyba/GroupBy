using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Invitation to group
    /// </summary>
    public class InvitationToGroup
    {
        /// <summary>
        /// Identificator of the invitation
        /// </summary>
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
