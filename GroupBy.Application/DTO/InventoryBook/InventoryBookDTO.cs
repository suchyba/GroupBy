using GroupBy.Application.DTO.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.InventoryBook
{
    public  class InventoryBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupSimpleDTO RelatedGroup { get; set; }
    }
}
