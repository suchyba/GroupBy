using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// Accounting book 
    /// </summary>
    public class AccountingBook
    {
        /// <summary>
        /// Accounting book identification number
        /// </summary>
        [Key, Column(Order = 0)]
        public int BookId { get; set; }
        /// <summary>
        /// Order number of this specific accounting book
        /// </summary>
        [Key, Column(Order = 1)]
        public int BookOrderNumber { get; set; }
        /// <summary>
        /// Name of the book (depends on owner specification)
        /// </summary>
        [Column(Order = 2)]
        public string Name { get; set; }
        /// <summary>
        /// If the user is unable to edit this book
        /// </summary>
        [Column(Order = 3)]
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
    }
}
