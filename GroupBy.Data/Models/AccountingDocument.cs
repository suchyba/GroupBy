using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// Financial type of document (for example invoice) <seealso cref="Document"/>
    /// </summary>
    public class AccountingDocument : Document
    {
        /// <summary>
        /// List of related record in accounting book <see cref="AccountingBook"/> <seealso cref="FinancialRecord"/>
        /// </summary>
        public virtual IEnumerable<FinancialRecord> RelatedRecords { get; set; }
    }
}
