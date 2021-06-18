using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryBook
{
    public class InventoryBookCreateDTO
    {
        public string Name { get; set; }
        public int RelatedGroupId { get; set; }
    }
}
