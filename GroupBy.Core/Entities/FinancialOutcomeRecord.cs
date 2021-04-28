using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain
{
    /// <summary>
    /// The financial record for outcome (for example invoice)
    /// </summary>
    public class FinancialOutcomeRecord : FinancialRecord
    {
        /// <summary>
        /// Inventory element (should be exact same value as in inventory book) <seealso cref="InventoryBook"/>
        /// </summary>
        public decimal? Inventory { get; set; }
        /// <summary>
        /// Bought item is some kind of material
        /// </summary>
        public decimal? Material { get; set; }
        /// <summary>
        /// Bought item is food (or food service)
        /// </summary>
        public decimal? Food { get; set; }
        /// <summary>
        /// Bougth item is some kind of service
        /// </summary>
        public decimal? Service { get; set; }
        /// <summary>
        /// Bought item is transport service
        /// </summary>
        public decimal? Transport { get; set; }
        /// <summary>
        /// Cost of insurance
        /// </summary>
        public decimal? Insurance { get; set; }
        /// <summary>
        /// Bought item is cost of accomodation
        /// </summary>
        public decimal? Accommodation { get; set; }
        /// <summary>
        /// Cost of salary
        /// </summary>
        public decimal? Salary { get; set; }
        /// <summary>
        /// Other outcomes
        /// </summary>
        public decimal? Other { get; set; }
        /// <summary>
        /// Override of abstract function (sums all sources) <seealso cref="FinancialRecord.CalculateTotalAmount"/> <seealso cref="FinancialRecord.TotalAmount"/>
        /// </summary>
        public override void CalculateTotalAmount()
        {
            TotalAmount = Inventory ?? 0 + Material ?? 0 + Food ?? 0 + Service ?? 0 + Transport ?? 0 + Insurance ?? 0 + Accommodation ?? 0 + Salary ?? 0 + Other ?? 0;
        }
    }
}
