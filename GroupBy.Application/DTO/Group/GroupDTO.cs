using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.DTO.Project;
using GroupBy.Application.DTO.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Group
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public VolunteerSimpleDTO Owner { get; set; }
        public GroupSimpleDTO ParentGroup { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
        public InventoryBookSimpleDTO InventoryBook { get; set; }
    }
}
