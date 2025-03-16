namespace GroupBy.Design.DTO.AccountingBookTemplate
{
    public class AccountingBookTemplateUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Guid> Categories { get; set; }
    }
}
