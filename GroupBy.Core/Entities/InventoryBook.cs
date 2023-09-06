using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Type represents Inventory book of the group <seealso cref="Group"/>
    /// </summary>
    public class InventoryBook
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the book
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Records of the book <seealso cref="InventoryBookRecord"/>
        /// </summary>
        public virtual IEnumerable<InventoryBookRecord> Records { get; set; }
        /// <summary>
        /// Groups which is owner of the items in this book
        /// </summary>
        [Required, ForeignKey("GroupId")]
        /// <summary>
        /// Group which is owner of the items in this book
        /// </summary>
        public virtual Group RelatedGroup { get; set; }
        [InverseProperty("DestinationInventoryBook")]
        /// <summary>
        /// Incoming item transfers to this book
        /// </summary>
        public virtual IEnumerable<InventoryItemTransfer> IncomingInventoryItemTransfers { get; set; }
        [InverseProperty("SourceInventoryBook")]
        /// <summary>
        /// Outgoing item transfers from this book
        /// </summary>
        public virtual IEnumerable<InventoryItemTransfer> OutgoingInventoryItemTransfers { get; set; }
    }
}
