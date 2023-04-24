using System;
using System.Collections.Generic;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// The reminder that could be set up in group <seealso cref="Element"/>
    /// </summary>
    public class Remainder : Element
    {
        /// <summary>
        /// Date and time then the reminder has to remind
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// The volunteers that saw that reminder already
        /// </summary>
        public virtual IEnumerable<Volunteer> VolunteersAccepted { get; set; }
    }
}
