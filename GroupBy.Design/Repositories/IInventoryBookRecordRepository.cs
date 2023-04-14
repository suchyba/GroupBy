using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IInventoryBookRecordRepository : IAsyncRepository<InventoryBookRecord>
    {
        public Task<IEnumerable<InventoryBookRecord>> TransferItemAsync(InventoryBookRecord inventoryBookFromRecord, InventoryBookRecord inventoryBookToRecord);
    }
}
