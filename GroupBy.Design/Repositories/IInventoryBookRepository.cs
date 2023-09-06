using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IInventoryBookRepository : IAsyncRepository<InventoryBook>
    {
        public Task<IEnumerable<InventoryBookRecord>> GetInventoryBookRecordsAsync(InventoryBook book, bool includeLocal = false);
        public Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync(InventoryBook book, bool includeLocal = false);
        public Task<IEnumerable<InventoryItemTransfer>> GetIncomingInventoryItemTransfersAsync(InventoryBook book, bool notConfirmedOnly = true, bool includeLocal = false);
    }
}
