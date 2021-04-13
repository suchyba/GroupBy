using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class AccountingBook
    {
        // klucz złożony
        public int BookId { get; set; }
        public int BookOrderNumber { get; set; }
        // tutaj
        public IEnumerable<FinancialRecord> Records { get; set; }
        public bool Locked { get; set; }
        public string Name { get; set; }
    }
}
