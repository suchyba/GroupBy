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
    /// Record in inventory book
    /// </summary>
    public class InventoryBookRecord
    {
        /// <summary>
        /// Part of the primary key. Identificator of the book  containing this record <see cref="Book"/>
        /// </summary>
        [Key, Column(Order = 0)]
        public int InventoryBookId { get; set; }
        /// <summary>
        /// Identificator of the record in the book
        /// </summary>
        [Key, Column(Order = 1)]
        public int Id { get; set; }
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
        /// Name of related document
        /// </summary>
        [Required]
        public string Document { get; set; }
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
        [ForeignKey("InventoryBookId"), Required]
        public virtual InventoryBook Book { get; set; }
    }
}
