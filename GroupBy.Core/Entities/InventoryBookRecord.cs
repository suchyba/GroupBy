﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Record in inventory book
    /// </summary>
    public class InventoryBookRecord
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Helps to order records
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        /// <summary>
        /// Related item <seealso cref="InventoryItem"/>
        /// </summary>
        [Required]
        public virtual InventoryItem Item { get; set; }
        /// <summary>
        /// Date of the record
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Related document <seealso cref="Document"/>
        /// </summary>
        [Required]
        public virtual Document Document { get; set; }
        /// <summary>
        /// Describes if the record is the income
        /// </summary>
        public bool Income { get; set; }
        /// <summary>
        /// Source of the item <seealso cref="InventoryItemSource"/>
        /// </summary>
        [Required]
        public virtual InventoryItemSource Source { get; set; }
        /// <summary>
        /// Book containing this record <seealso cref="InventoryBook"/>
        /// </summary>
        [Required, ForeignKey("InventoryBookId")]
        public virtual InventoryBook Book { get; set; }
    }
}
