using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Design.DTO.FinancialOutcomeRecord
{
    public class FinancialOutcomeRecordUpdateDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public IEnumerable<FinancialCategoryValueUpdateDTO> Values { get; set; }
    }
}
