using GroupBy.Design.DTO.FinancialCategory;

namespace GroupBy.Design.DTO.AccountingBookTemplate
{
    public class AccountingBookTemplateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<FinancialCategoryDTO> Categories { get; set; }
    }
}
