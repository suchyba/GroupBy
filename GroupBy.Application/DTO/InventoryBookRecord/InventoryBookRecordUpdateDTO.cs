using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryBookRecord
{
    public class InventoryBookRecordUpdateDTO
    {
        public int Id { get; set; }
        public int InventoryBookId { get; set; }
        public int ItemId { get; set; }
        public DateTime Date { get; set; }
        public int DocumentId { get; set; }
        public bool Income { get; set; }
        public int SourceId { get; set; }
    }
}
