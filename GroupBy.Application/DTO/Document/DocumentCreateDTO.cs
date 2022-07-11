using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Document
{
    public class DocumentCreateDTO
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<int> GroupsId { get; set; }
        public int? RelatedProjectId { get; set; }
    }
}
