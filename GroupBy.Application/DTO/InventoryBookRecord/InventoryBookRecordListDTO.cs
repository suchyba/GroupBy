using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Application.DTO.InventoryItemSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryBookRecord
{
    public class InventoryBookRecordListDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Document { get; set; }
        public bool Income { get; set; }
        public InventoryItemSimpleDTO Item { get; set; }
        public InventoryItemSourceDTO Source { get; set; }
    }
}
