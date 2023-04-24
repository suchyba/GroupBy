using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.Project;

namespace GroupBy.Design.TO.FinancialOutcomeRecord
{
    public class FinancialOutcomeRecordDTO
    {
        public Guid Id { get; set; }
        public decimal? Inventory { get; set; }
        public decimal? Material { get; set; }
        public decimal? Service { get; set; }
        public decimal? Transport { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? Accommodation { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Food { get; set; }
        public decimal? Other { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
        public AccountingDocumentSimpleDTO RelatedDocument { get; set; }
        public AccountingBookSimpleDTO Book { get; set; }
    }
}
