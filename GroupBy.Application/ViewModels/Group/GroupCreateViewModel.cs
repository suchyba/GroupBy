using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.ViewModels.Group
{
    public class GroupCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentGroupId { get; set; }
        public int OwnerId { get; set; }
        public int? ProjectId { get; set; }
    }
}
