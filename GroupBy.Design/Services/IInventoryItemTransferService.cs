using GroupBy.Design.DTO.InventoryItemTransfer;

namespace GroupBy.Design.Services
{
    public interface IInventoryItemTransferService : IAsyncService<InventoryItemTransferSimpleDTO, InventoryItemTransferDTO, InventoryItemTransferCreateDTO, InventoryItemTransferUpdateDTO>
    {
        public Task<InventoryItemTransferDTO> ConfirmTransferAsync(InventoryItemTransferConfirmDTO model);
        public Task CancelTransferAsync(Guid id);
    }
}
