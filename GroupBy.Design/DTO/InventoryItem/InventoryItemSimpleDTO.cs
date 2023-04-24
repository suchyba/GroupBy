namespace GroupBy.Design.TO.InventoryItem
{
    public class InventoryItemSimpleDTO
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
