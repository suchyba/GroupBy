using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryBookRecord
{
    public class InventoryBookRecordTransferDTO
    {
        public int InventoryBookFromId { get; set; }
        public int InventoryBookToId { get; set; }
        public int ItemId { get; set; }
        public DateTime Date { get; set; }
        public int DocumentId { get; set; }
        public int SourceFromId { get; set; }
        public int SourceToId { get; set; }
    }
}
