using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Is the type describing the type of the inventory book record <seealso cref="InventoryBookRecord"/>
    /// </summary>
    public class InventoryItemSource
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the type
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
