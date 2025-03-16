namespace GroupBy.Design.DTO.FinancialCategoryValue
{
    public class FinancialCategoryValueUpdateDTO
    {
        public Guid? Id { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Value { get; set; }
    }
}
