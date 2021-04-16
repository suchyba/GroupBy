using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// The rank (some kind of tag 
    /// </summary>
    public class Rank
    {
        /// <summary>
        /// The identificator
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the rank
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Higher-level rank
        /// </summary>
        public virtual Rank HigherRank { get; set; }
        /// <summary>
        /// List of volunteers has this rank
        /// </summary>
        public virtual IEnumerable<Volunteer> VolunteersRanked { get; set; }
    }
}