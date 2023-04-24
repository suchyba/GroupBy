namespace GroupBy.Design.TO.AccountingBook
{
    public class AccountingBookSimpleDTO
    {
        public Guid Id { get; set; }
        public int BookIdentificator { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
    }
}
