namespace GroupBy.Design.DTO.AccountingBook
{
    public class AccountingBookCreateDTO
    {
        public int BookIdentificator { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
        public Guid RelatedGroupId { get; set; }
    }
}
