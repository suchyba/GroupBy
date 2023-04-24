using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Position of the volunteer <seealso cref="Volunteer"/>
    /// </summary>
    public class Position
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the position
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Higher-level position (if this is the highest-level set to null)
        /// </summary>
        public Position HigherPosition { get; set; }
    }
}
