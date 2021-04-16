using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// The resolution adopted in the group <see cref="Group"/>
    /// </summary>
    public class Resolution
    {
        /// <summary>
        /// Group identificator <see cref="Group"/>
        /// </summary>
        [Key, Column(Order = 0)]
        public int GroupId { get; set; }
        /// <summary>
        /// The identification symbol of the resolution
        /// </summary>
        [Key, Column(Order = 1)]
        public string Symbol { get; set; }
        /// <summary>
        /// Date when the resolution has been adopted
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The content of the resolution
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// The legistrator of this resolution <seealso cref="Volunteer"/>
        /// </summary>
        [Required]
        public virtual Volunteer Legislator { get; set; }
        /// <summary>
        /// Group where that resolution has been adopted <seealso cref="Group"/>
        /// </summary>
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}