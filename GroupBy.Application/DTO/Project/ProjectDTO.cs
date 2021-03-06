using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Project
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        public bool Independent { get; set; }
        public GroupSimpleDTO ParentGroup { get; set; }
        public GroupSimpleDTO ProjectGroup { get; set; }
        public VolunteerSimpleDTO Owner { get; set; }
    }
}
