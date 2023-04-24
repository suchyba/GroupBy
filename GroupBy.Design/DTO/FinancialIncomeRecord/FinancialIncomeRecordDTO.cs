using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.Project;

namespace GroupBy.Design.TO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordDTO
    {
        public Guid Id { get; set; }
        public decimal? MembershipFee { get; set; }
        public decimal? ProgramFee { get; set; }
        public decimal? Dotation { get; set; }
        public decimal? EarningAction { get; set; }
        public decimal? OnePercent { get; set; }
        public decimal? Other { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
        public AccountingDocumentSimpleDTO RelatedDocument { get; set; }
        public AccountingBookSimpleDTO Book { get; set; }
    }
}
