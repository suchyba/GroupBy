using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain
{
    /// <summary>
    /// Agreement to accept by volunteer <seealso cref="Volunteer"/>
    /// </summary>
    public class Agreement
    {
        /// <summary>
        /// Primary key
        /// </summary>
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
