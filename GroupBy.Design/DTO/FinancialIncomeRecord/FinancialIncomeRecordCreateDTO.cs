using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Design.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordCreateDTO
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid BookId { get; set; }
        public Guid? RelatedProjectId { get; set; }
        public Guid RelatedDocumentId { get; set; }
        public IEnumerable<FinancialCategoryValueCreateDTO> Values { get; set; }
    }
}
