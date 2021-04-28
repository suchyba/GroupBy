using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain
{
    /// <summary>
    /// Represents group of volunteers <seealso cref="Volunteer"/>
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Identificator of the group
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the group
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description of the group
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Higher-level group (if is set to null it is the highest-level group)
        /// </summary>
        public virtual Group ParentGroup { get; set; }
        /// <summary>
        /// List of lower-level groups
        /// </summary>
        [InverseProperty("ParentGroup")]
        public virtual IEnumerable<Group> ChildGroups { get; set; }
        /// <summary>
        /// The owner of the group
        /// </summary>
        [Required, InverseProperty("OwnedGroups")]
        public virtual Volunteer Owner { get; set; }
        /// <summary>
        /// List of group members <seealso cref="Volunteer"/>
        /// </summary>
        public virtual IEnumerable<Volunteer> Members { get; set; }
        /// <summary>
        /// Accounting books of the group <seealso cref="AccountingBook"/>
        /// </summary>
        public virtual IEnumerable<AccountingBook> AccountingBooks { get; set; }
        /// <summary>
        /// Resolutions of the administration of the group <seealso cref="Resolution"/>
        /// </summary>
        public virtual IEnumerable<Resolution> Resolutions { get; set; }
        /// <summary>
        /// Inventory book of the group <seealso cref="Models.InventoryBook"/>
        /// </summary>
        public virtual InventoryBook InventoryBook { get; set; }
        public int? ProjectId { get; set; }
        /// <summary>
        /// This is the project in which this group is project group  <seealso cref="Project"/>
        /// </summary>
        [InverseProperty("ProjectGroup"), ForeignKey("ProjectId")]
        public virtual Project RelatedProject { get; set; }
        /// <summary>
        /// List of projects realised in this group <seealso cref="Project"/>
        /// </summary>
        [InverseProperty("ParentGroup")]
        public virtual IEnumerable<Project> ProjectsRealisedInGroup { get; set; }
        /// <summary>
        /// List of interactive elements added by members (for example reminder) <seealso cref="Element"/>
        /// </summary>
        public virtual IEnumerable<Element> Elements { get; set; }
        /// <summary>
        /// Permissions for positions
        /// </summary>
        public virtual IEnumerable<GroupsPermissions> Permissions { get; set; }
    }
}
