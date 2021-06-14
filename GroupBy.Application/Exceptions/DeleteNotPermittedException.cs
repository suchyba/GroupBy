using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Exceptions
{
    public class DeleteNotPermittedException : ApplicationException
    {
        public DeleteNotPermittedException(string name) : base($"Cannot delete {name}, because it's used by other object.")
        {

        }
    }
}
