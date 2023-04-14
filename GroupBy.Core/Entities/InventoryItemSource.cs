using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Is the type describing the type of the inventory book record <seealso cref="InventoryBookRecord"/>
    /// </summary>
    public class InventoryItemSource
    {
        /// <summary>
        /// Identificator
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the type
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
