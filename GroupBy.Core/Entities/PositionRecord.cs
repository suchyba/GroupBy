using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Record describing when the volunteer has been appointed on and dismissed off the position
    /// </summary>
    public class PositionRecord
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VolunteerId { get; set; }
        /// <summary>
        /// Position which this record references
        /// </summary>
        [Required]
        public virtual Position Position { get; set; }
        /// <summary>
        /// The date when the person has been appointed on the position
        /// </summary>
        public DateTime AppointmentDate { get; set; }
        /// <summary>
        /// The date when the person has been dismissed from the position (if has been)
        /// </summary>
        public DateTime? DismissDate { get; set; }
        /// <summary>
        /// The resolution that is appointing the person on a position <seealso cref="Resolution"/>
        /// </summary>
        public virtual Resolution AppointingResolution { get; set; }
        /// <summary>
        /// The resolution that is dismissing the person from a position <seealso cref="Resolution"/>
        /// </summary>
        public virtual Resolution DismissingResolution { get; set; }
        /// <summary>
        /// Groups where the person has been appointed on a position
        /// </summary>
        [Required]
        public virtual Group RelatedGroup { get; set; }
        /// <summary>
        /// The person
        /// </summary>
        [Key]
        [Required, ForeignKey("VolunteerId")]
        public virtual Volunteer Person { get; set; }
    }
}