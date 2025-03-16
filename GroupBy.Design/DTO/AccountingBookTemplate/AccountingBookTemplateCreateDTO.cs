using GroupBy.Design.DTO.FinancialCategory;

namespace GroupBy.Design.DTO.AccountingBookTemplate
{
    public class AccountingBookTemplateCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Guid> Categories { get; set; }
    }
}
