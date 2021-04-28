using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain
{
    /// <summary>
    /// Item mentioned in inventory book <seealso cref="InventoryBook"/>
    /// </summary>
    public class InventoryItem
    {
        [Key]
        public Guid Id { get; set; }
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
        public decimal Value { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        public string Descryption { get; set; }
        /// <summary>
        /// History of the item from all the books <seealso cref="InventoryBook"/> <seealso cref="InventoryBookRecord"/>
        /// </summary>
        public virtual IEnumerable<InventoryBookRecord> History { get; set; }
    }
}
