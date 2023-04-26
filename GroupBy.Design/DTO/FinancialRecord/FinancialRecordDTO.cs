using GroupBy.Design.DTO.Document;
using GroupBy.Design.DTO.Project;

namespace GroupBy.Design.DTO.FinancialRecord
{
    public class FinancialRecordDTO : FinancialRecordSimpleDTO
    {
        public ProjectSimpleDTO RelatedProject { get; set; }
        public DocumentDTO RelatedDocument { get; set; }
    }
}
