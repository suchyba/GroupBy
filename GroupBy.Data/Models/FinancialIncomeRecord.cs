using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// Financial record for income (for example membership fee) <seealso cref="FinancialRecord"/>
    /// </summary>
    public class FinancialIncomeRecord : FinancialRecord
    {
        /// <summary>
        /// Membership fee 
        /// </summary>
        public decimal? MembershipFee { get; set; }
        /// <summary>
        /// Program fee (for example for project <seealso cref="Project"/>)
        /// </summary>
        public decimal? ProgramFee { get; set; }
        /// <summary>
        /// Dotation
        /// </summary>
        public decimal? Dotation { get; set; }
        /// <summary>
        /// Money from earning action
        /// </summary>
        public decimal? EarningAction { get; set; }
        /// <summary>
        /// Money from one procent
        /// </summary>
        public decimal? OnePercent { get; set; }
        /// <summary>
        /// Other money source
        /// </summary>
        public decimal? Other { get; set; }
        /// <summary>
        /// Override of abstract function (sums all sources) <seealso cref="FinancialRecord.CalculateTotalAmount"/> <seealso cref="FinancialRecord.TotalAmount"/>
        /// </summary>
        public override void CalculateTotalAmount()
        {
            TotalAmount = MembershipFee ?? 0 + ProgramFee ?? 0 + Dotation ?? 0 + EarningAction ?? 0 + OnePercent ?? 0 + Other ?? 0;
        }
    }
}
