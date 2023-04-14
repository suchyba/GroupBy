using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Project;

namespace GroupBy.Design.TO.Document
{
    public class DocumentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<GroupSimpleDTO> Groups { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
    }
}
