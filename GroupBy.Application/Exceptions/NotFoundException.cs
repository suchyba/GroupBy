using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public object Key { get; set; }
        public NotFoundException(string name, object key) : base($"{name} ({key}) is not found")
        {
            Key = key;
        }
    }
}
