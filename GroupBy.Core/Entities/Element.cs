using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Abstract type for interactive element which group members can add
    /// </summary>
    public abstract class Element
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Groups in which this element is added <seealso cref="Models.Group"/>
        /// </summary>
        [Required]
        public IEnumerable<Group> Groups { get; set; }
        /// <summary>
        /// Name of this element
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The project related with this element (if it's related) <seealso cref="Project"/>
        /// </summary>
        public virtual Project RelatedProject { get; set; }
    }
}