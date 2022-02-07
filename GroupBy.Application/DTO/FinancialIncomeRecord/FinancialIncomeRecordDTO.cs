using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Application.DTO.Project;
using System;

namespace GroupBy.Application.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordDTO
    {
        public int Id { get; set; }
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
