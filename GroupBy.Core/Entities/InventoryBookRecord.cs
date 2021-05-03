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
    /// Record in inventory book
    /// </summary>
    public class InventoryBookRecord
    {
        /// <summary>
        /// Identificator of the record in the book
        /// </summary>
        public int Id { get; set; }
        public int InventoryBookId { get; set; }
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
        [Required]
        public int OrderNumber { get; set; }
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
