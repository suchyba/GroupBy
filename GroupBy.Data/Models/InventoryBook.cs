using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class InventoryBook
    {
        [Key]
        public int Id { get; set; }
        public IEnumerable<InventoryBookRecord> Records { get; set; }
    }
}
