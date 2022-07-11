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
        public virtual Group RelatedGroup { get; set; }
    }
}
