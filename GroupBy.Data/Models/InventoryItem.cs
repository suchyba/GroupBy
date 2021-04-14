using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class InventoryItem
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Descryption { get; set; }
        public IEnumerable<InventoryBookRecord> History { get; set; }
    }
}
