using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IInventoryItemRepository : IAsyncRepository<InventoryItem>
    {
        public Task<IEnumerable<InventoryBookRecord>> GetInventoryItemHistoryAsync(Guid itemId, bool includeLocal = false);
        public Task<IEnumerable<InventoryItem>> GetInventoryItemWithoutHistory(bool includeLocal = false);
    }
}
