using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryItem
{
    public class InventoryItemSimpleDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
