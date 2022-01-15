using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Financial record for income (for example membership fee) <seealso cref="FinancialRecord"/>
    /// </summary>
    public class FinancialIncomeRecord : FinancialRecord
    {
        /// <summary>
        /// Membership fee 
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MembershipFee { get; set; }
        /// <summary>
        /// Program fee (for example for project <seealso cref="Project"/>)
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ProgramFee { get; set; }
        /// <summary>
        /// Dotation
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Dotation { get; set; }
        /// <summary>
        /// Money from earning action
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? EarningAction { get; set; }
        /// <summary>
        /// Money from one procent
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? OnePercent { get; set; }
        /// <summary>
        /// Other money source
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Other { get; set; }
        /// <summary>
        /// Sum of all variables
        /// </summary>
        /// <returns></returns>
        public override decimal GetTotal()
        {
            return (MembershipFee ?? 0) + (ProgramFee ?? 0) + (Dotation ?? 0) + (EarningAction ?? 0) + (OnePercent ?? 0) + (Other ?? 0);
        }
    }
}
