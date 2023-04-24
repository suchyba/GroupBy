using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// To-do list element
    /// </summary>
    public class TODOListElement
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// The task content
        /// </summary>
        [Required]
        public string Task { get; set; }
        /// <summary>
        /// The order numner on the list
        /// </summary>
        public int OrderNumber { get; set; }
        /// <summary>
        /// Subtasks list
        /// </summary>
        public virtual IEnumerable<TODOListElement> SubTasks { get; set; }
        /// <summary>
        /// The volunteer assigned to complete this task <seealso cref="Volunteer"/>
        /// </summary>
        public virtual Volunteer Asignee { get; set; }
        /// <summary>
        /// Deadline of this task
        /// </summary>
        public DateTime? Deadline { get; set; }
    }
}