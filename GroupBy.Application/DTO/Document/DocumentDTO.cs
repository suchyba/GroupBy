using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Document
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<GroupSimpleDTO> Groups { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
    }
}
