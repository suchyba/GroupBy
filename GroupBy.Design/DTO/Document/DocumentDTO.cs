using GroupBy.Design.DTO.Group;
using GroupBy.Design.DTO.Project;

namespace GroupBy.Design.DTO.Document
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
