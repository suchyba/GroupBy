using GroupBy.Design.DTO.Group;

namespace GroupBy.Design.DTO.AccountingBook
{
    public class AccountingBookDTO
    {
        public Guid Id { get; set; }
        public int BookIdentificator { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
        public GroupSimpleDTO RelatedGroup { get; set; }
        public decimal Balance { get; set; }
    }
}
