using GroupBy.Design.DTO.InventoryBook;
using GroupBy.Design.DTO.Project;
using GroupBy.Design.DTO.Volunteer;

namespace GroupBy.Design.DTO.Group
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public VolunteerSimpleDTO Owner { get; set; }
        public GroupSimpleDTO ParentGroup { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
        public InventoryBookSimpleDTO InventoryBook { get; set; }
    }
}
