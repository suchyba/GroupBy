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
    /// Item mentioned in inventory book <seealso cref="InventoryBook"/>
    /// </summary>
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Identification symbol of the item
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Name of the item
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Value of the item
        /// </summary>
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// History of the item from all the books <seealso cref="InventoryBook"/> <seealso cref="InventoryBookRecord"/>
        /// </summary>
        public virtual IEnumerable<InventoryBookRecord> History { get; set; }
    }
}
