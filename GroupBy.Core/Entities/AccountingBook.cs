using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Accounting book 
    /// </summary>
    public class AccountingBook
    {
        /// <summary>
        /// Accounting book identification number
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// Order number of this specific accounting book
        /// </summary>
        public int BookOrderNumberId { get; set; }
        /// <summary>
        /// Name of the book (depends on owner specification)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// If the user is unable to edit this book
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// Records in the book <seealso cref="FinancialRecord"/>
        /// </summary>
        public virtual IEnumerable<FinancialRecord> Records { get; set; }
        /// <summary>
        /// Group which is owner of this book <seealso cref="Group"/>
        /// </summary>
        [Required]
        public virtual Group RelatedGroup { get; set; }
        /// <summary>
        /// Balance.
        /// </summary>
        /// <returns>Sum of values.</returns>
        public decimal GetBalance()
        {
            decimal balance = 0;
            foreach (FinancialRecord record in Records)
            {
                if (record is FinancialIncomeRecord)
                    balance += record.GetTotal();
                else if (record is FinancialOutcomeRecord)
                    balance -= record.GetTotal();
            }
            return balance;
        }
    }
}
