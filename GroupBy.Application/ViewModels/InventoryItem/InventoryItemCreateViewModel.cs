using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.ViewModels.InventoryItem
{
    public class InventoryItemCreateViewModel
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Descryption { get; set; }
    }
}
