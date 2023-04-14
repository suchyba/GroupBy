namespace GroupBy.Design.TO.InventoryBookRecord
{
    public class InventoryBookRecordSimpleDTO
    {
        public Guid Id { get; set; }
        public Guid InventoryBookId { get; set; }
        public DateTime Date { get; set; }
        public bool Income { get; set; }
    }
}
