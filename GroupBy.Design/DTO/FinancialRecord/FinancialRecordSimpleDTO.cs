using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Design.DTO.FinancialRecord
{
    public class FinancialRecordSimpleDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string RelatedDocumentName { get; set; }
        public string RelatedProjectName { get; set; }
        public IEnumerable<FinancialCategoryValueSimpleDTO> Values { get; set; }
        public decimal Total { get; set; }
    }
}
