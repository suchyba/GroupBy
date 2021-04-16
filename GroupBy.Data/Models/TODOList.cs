using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// The element representing a to-do list <seealso cref="Element"/>
    /// </summary>
    public class TODOList : Element
    {
        /// <summary>
        /// The elements on the list <seealso cref="TODOListElement"/>
        /// </summary>
        public virtual IEnumerable<TODOListElement> List { get; set; }
    }
}
