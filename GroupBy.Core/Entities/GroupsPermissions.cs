using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Represent the group of permissions in group for user with specific position
    /// </summary>
    public class GroupsPermissions
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid PositionId { get; set; }
        /// <summary>
        /// Permission to invitate members to group <seealso cref="Invitation"/>
        /// </summary>
        public bool? Invitation { get; set; }
        /// <summary>
        /// Permission to see elements posted in the group <seealso cref="Element"/>
        /// </summary>
        public bool? SeeElements { get; set; }
        /// <summary>
        /// Permission to add and edit elements posted in group <seealso cref="Element"/>
        /// </summary>
        public bool? EditElements { get; set; }
        /// <summary>
        /// Permission to see and edit accounting and inventory books of the group <seealso cref="AccountingBook"/> <seealso cref="InventoryBook"/>
        /// </summary>
        public bool? Books { get; set; }
        /// <summary>
        /// Permission to remove or add members to the group
        /// </summary>
        public bool? EditMembers { get; set; }
        /// <summary>
        /// Permission to add and edit resolutions of the group <seealso cref="Resolution"/>
        /// </summary>
        public bool? Resolutions { get; set; }
        /// <summary>
        /// Permission to add, edit and remove subgroups in group
        /// </summary>
        public bool? Subgroups { get; set; }
        /// <summary>
        /// Permission to edit positions of the members
        /// </summary>
        public bool? EditPosition { get; set; }
        /// <summary>
        /// Groups where this permissions are valid <seealso cref="Models.Group"/>
        /// </summary>
        [Required, ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        /// <summary>
        /// Position which you must have to use this permissions <seealso cref="Models.Position"/>
        /// </summary>
        [Required, ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
    }
}
