using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Group
{
    public class GroupCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentGroupId { get; set; }
        public int OwnerId { get; set; }
    }
}
