using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Code that can be generated and when typed in registration form some proporties will be setted
    /// </summary>
    public class RegistrationCode
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The cliphered key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Name for easier identification
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Group where the volunteer will be added <seealso cref="Group"/>
        /// </summary>
        [Required]
        public virtual Group TargetGroup { get; set; }
        /// <summary>
        /// Rank that will be added to a person who use this code <seealso cref="Rank"/>
        /// </summary>
        public virtual Rank TargetRank { get; set; }
        /// <summary>
        /// Owner of this code
        /// </summary>
        public virtual Volunteer Owner { get; set; }
    }
}
