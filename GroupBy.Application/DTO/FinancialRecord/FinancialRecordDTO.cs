using GroupBy.Application.DTO.Document;
using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.FinancialRecord
{
    public class FinancialRecordDTO : FinancialRecordSimpleDTO
    {
        public ProjectSimpleDTO RelatedProject { get; set; }
        public DocumentDTO RelatedDocument { get; set; }
    }
}
