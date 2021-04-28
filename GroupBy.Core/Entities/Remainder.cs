using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain
{
    /// <summary>
    /// The reminder that could be setted up in group <seealso cref="Element"/>
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
