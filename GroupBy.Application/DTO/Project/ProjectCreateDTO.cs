using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Project
{
    public class ProjectCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        public bool Independent { get; set; }
        public int? ProjectGroupId { get; set; }
        public int ParentGroupId { get; set; }
        public int OwnerId { get; set; }
    }
}
