namespace GroupBy.Design.TO.AccountingBook
{
    public class AccountingBookCreateDTO
    {
        public int BookId { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
        public int RelatedGroupId { get; set; }
    }
}
