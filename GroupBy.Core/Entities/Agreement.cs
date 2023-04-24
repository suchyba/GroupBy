using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Agreement to accept by volunteer <seealso cref="Volunteer"/>
    /// </summary>
    public class Agreement
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Content of agreement
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Volunteers who accepted this agreement
        /// </summary>
        public virtual IEnumerable<Volunteer> VolunteersAccepted { get; set; }
    }
}
