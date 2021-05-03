using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.ViewModels
{
    public class AccountingBookViewModel
    {
        public int BookId { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
    }
}
