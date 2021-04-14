using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class InventoryBookRecord
    {
        // klucz
        public int InventoryBookId { get; set; }
        public int Id { get; set; }
        // klucz
        public InventoryItem Item { get; set; }
        public DateTime Date { get; set; }
        public string Document { get; set; }
        public bool Income { get; set; }
        public InventoryItemSource Source { get; set; }
    }
}
