using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class FinancialIncomeRecord : FinancialRecord
    {
        public decimal? MembershipFee { get; set; }
        public decimal? ProgramFee { get; set; }
        public decimal? Dotation { get; set; }
        public decimal? EarningAction { get; set; }
        public decimal? OnePercent { get; set; }
        public decimal? Other { get; set; }
        public override void CalculateTotalAmount()
        {
            TotalAmount = MembershipFee ?? 0 + ProgramFee ?? 0 + Dotation ?? 0 + EarningAction ?? 0 + OnePercent ?? 0 + Other ?? 0;
        }
    }
}
