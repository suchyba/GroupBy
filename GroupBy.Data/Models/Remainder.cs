using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class Remainder : Element
    {
        public DateTime DateTime { get; set; }
        public IEnumerable<Volunteer> VolunteersAccepted { get; set; }
    }
}
