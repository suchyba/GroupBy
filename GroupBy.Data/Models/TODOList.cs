using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class TODOList : Element
    {
        public IEnumerable<TODOListElement> List { get; set; }
    }
}
