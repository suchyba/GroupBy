using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// Position of the volounteer <seealso cref="Volunteer"/>
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Identiicator
        /// </summary>
        [Key]
        public int Id { get; set; }
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
