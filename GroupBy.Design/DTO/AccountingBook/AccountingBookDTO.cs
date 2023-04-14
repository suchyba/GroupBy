using GroupBy.Design.TO.Group;

namespace GroupBy.Design.TO.AccountingBook
{
    public class AccountingBookDTO
    {
        public int BookId { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
        public GroupSimpleDTO RelatedGroup { get; set; }
        public decimal Balance { get; set; }
    }
}
