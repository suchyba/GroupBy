using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryBookRecord
{
    public class InventoryBookRecordSimpleDTO
    {
        public int Id { get; set; }
        public int InventoryBookId { get; set; }
        public DateTime Date { get; set; }
        public bool Income { get; set; }
    }
}
