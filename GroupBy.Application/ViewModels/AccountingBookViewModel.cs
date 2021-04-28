using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.ViewModels
{
    public class AccountingBookViewModel
    {
        public Guid Id { get; set; }
        public int BookNumber { get; set; }
        public int BookOrderNumber { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
    }
}
