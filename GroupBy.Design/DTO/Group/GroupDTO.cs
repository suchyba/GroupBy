using GroupBy.Design.TO.InventoryBook;
using GroupBy.Design.TO.Project;
using GroupBy.Design.TO.Volunteer;

namespace GroupBy.Design.TO.Group
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
