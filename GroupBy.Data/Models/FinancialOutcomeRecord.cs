using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class FinancialOutcomeRecord : FinancialRecord
    {
        public decimal? Inventory { get; set; }
        public decimal? Material { get; set; }
        public decimal? Food { get; set; }
        public decimal? Service { get; set; }
        public decimal? Transport { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? Accommodation { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Other { get; set; }
        public override void CalculateTotalAmount()
        {
            TotalAmount = Inventory ?? 0 + Material ?? 0 + Food ?? 0 + Service ?? 0 + Transport ?? 0 + Insurance ?? 0 + Accommodation ?? 0 + Salary ?? 0 + Other ?? 0;
        }
    }
}
