using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain.Entities
{
    public class InventoryItemTransfer
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Item to be transfered.
        /// </summary>
        [Required]
        public Guid OutcomeInventoryBookRecordId { get; set; }
        /// <summary>
        /// Id of the record in the inventory book where the item is put to <see cref="IncomeInventoryBookRecord"/>
        /// </summary>
        public Guid? IncomeInventoryBookRecordId { get; set; }
        /// <summary>
        /// Creation date of the transfer
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Date when the receiver of the item confirmed the transfer
        /// </summary>
        public DateTime? ConfirmationDate { get; set; }
        /// <summary>
        /// If the receiver of the item confirmed the transfer
        /// </summary>
        public bool ConfirmedByReceiver => IncomeInventoryBookRecordId != null;
        /// <summary>
        /// Record in the inventory book where the item is taken from
        /// </summary>
        [Required, ForeignKey("OutcomeInventoryBookRecordId")]
        public virtual InventoryBookRecord OutcomeInventoryBookRecord { get; set; }
        /// <summary>
        /// Record in the inventory book where the item is put to (if it already happened)
        /// </summary>
        [ForeignKey("IncomeInventoryBookRecordId")]
        public virtual InventoryBookRecord IncomeInventoryBookRecord { get; set; }
        /// <summary>
        /// Inventory book from where the item is taken
        /// </summary>
        [Required, InverseProperty("OutgoingInventoryItemTransfers")]
        public virtual InventoryBook SourceInventoryBook { get; set; }
        /// <summary>
        /// Inventory book where the item is put to
        /// </summary>
        [Required, InverseProperty("IncomingInventoryItemTransfers")]
        public virtual InventoryBook DestinationInventoryBook { get; set; }
    }
}
