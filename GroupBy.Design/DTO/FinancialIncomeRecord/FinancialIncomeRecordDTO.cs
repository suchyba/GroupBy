using GroupBy.Design.DTO.AccountingBook;
using GroupBy.Design.DTO.AccountingDocument;
using GroupBy.Design.DTO.FinancialCategoryValue;
using GroupBy.Design.DTO.Project;

namespace GroupBy.Design.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordDTO
    {
        public Guid Id { get; set; }
       
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
        public AccountingDocumentSimpleDTO RelatedDocument { get; set; }
        public AccountingBookSimpleDTO Book { get; set; }
        public IEnumerable<FinancialCategoryValueSimpleDTO> Values { get; set; }
    }
}
