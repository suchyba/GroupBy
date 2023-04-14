using GroupBy.Design.TO.Document;
using GroupBy.Design.TO.Project;

namespace GroupBy.Design.TO.FinancialRecord
{
    public class FinancialRecordDTO : FinancialRecordSimpleDTO
    {
        public ProjectSimpleDTO RelatedProject { get; set; }
        public DocumentDTO RelatedDocument { get; set; }
    }
}
