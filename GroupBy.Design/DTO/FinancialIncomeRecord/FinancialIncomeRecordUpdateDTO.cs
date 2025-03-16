using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Design.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordUpdateDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public IEnumerable<FinancialCategoryValueUpdateDTO> Values { get; set; }
    }
}
