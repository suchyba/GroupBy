using GroupBy.Design.DTO.InventoryBookRecord;

namespace GroupBy.Design.Services
{
    public interface IInventoryBookRecordService : IAsyncService<InventoryBookRecordSimpleDTO, InventoryBookRecordSimpleDTO, InventoryBookRecordCreateDTO, InventoryBookRecordUpdateDTO>
    {
        public Task<IEnumerable<InventoryBookRecordDTO>> TransferItemAsync(InventoryBookRecordTransferDTO record);
    }
}
