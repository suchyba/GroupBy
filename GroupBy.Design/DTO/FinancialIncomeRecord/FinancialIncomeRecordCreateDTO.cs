namespace GroupBy.Design.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordCreateDTO
    {
        public decimal? MembershipFee { get; set; }
        public decimal? ProgramFee { get; set; }
        public decimal? Dotation { get; set; }
        public decimal? EarningAction { get; set; }
        public decimal? OnePercent { get; set; }
        public decimal? Other { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid BookId { get; set; }
        public Guid? RelatedProjectId { get; set; }
        public Guid RelatedDocumentId { get; set; }
    }
}
