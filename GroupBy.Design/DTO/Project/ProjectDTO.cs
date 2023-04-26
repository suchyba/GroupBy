using GroupBy.Design.DTO.Group;
using GroupBy.Design.DTO.Volunteer;

namespace GroupBy.Design.DTO.Project
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
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
