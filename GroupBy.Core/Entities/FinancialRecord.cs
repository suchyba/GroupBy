using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// The abstract type of the record in accounting book (<seealso cref="AccountingBook"/>)
    /// </summary>
    public abstract class FinancialRecord
    {
        [Key]
        public Guid Id { get; set; }
        public int BookIdentificator { get => Book?.BookIdentificator ?? -1; }
        public int BookOrderNumberId { get => Book?.BookOrderNumberId ?? -1; }
        /// <summary>
        /// Total value.
        /// </summary>
        /// <returns>Sum of values.</returns>
        public abstract decimal GetTotal();
        /// <summary>
        /// Date of the record
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Description of the record
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Accounting book where this record is located (it is also part of primary key: <see cref="BookIdentificator"/>, <see cref="BookOrderNumber"/>)<seealso cref="AccountingBook"/>
        /// </summary>
        [Required]
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