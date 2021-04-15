using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    /// <summary>
    /// Type represents Inventory book of the group <seealso cref="Group"/>
    /// </summary>
    public class InventoryBook
    {
        /// <summary>
        /// Identificator of book
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Records of the book <seealso cref="InventoryBookRecord"/>
        /// </summary>
        public virtual IEnumerable<InventoryBookRecord> Records { get; set; }
        /// <summary>
        /// Group which is owner of the items in this book
        /// </summary>
        [Required]
        public virtual Group RelatedGroup { get; set; }
    }
}
