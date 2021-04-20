using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// The abstract type of the record in accounting book (<seealso cref="AccountingBook"/>)
    /// </summary>
    public abstract class FinancialRecord
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
        /// Order number in the book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Summary income/outcome (read only - need to refresh by calling <see cref="CalculateTotalAmount"/>)
        /// </summary>
        public decimal TotalAmount { get; protected set; }
        /// <summary>
        /// Refreshs <see cref="TotalAmount"/>
        /// </summary>
        public abstract void CalculateTotalAmount();
        /// <summary>
        /// Date of the record
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Description of the record
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Accounting book where this record is located (it is also part of primary key: <see cref="BookId"/>, <see cref="BookOrderNumber"/>)<seealso cref="AccountingBook"/>
        /// </summary>
        [Required, ForeignKey("BookId, BookOrderNumberId")]
        public virtual AccountingBook Book { get; set; }
        /// <summary>
        /// If this record is associated with the project, here is the tag <seealso cref="Project"/>
        /// </summary>
        public virtual Project RelatedProject { get; set; }
        /// <summary>
        /// Document in what this record is mentioned <seealso cref="AccountingDocument"/>
        /// </summary>
        [Required]
        public virtual AccountingDocument RelatedDocument { get; set; }
    }
}