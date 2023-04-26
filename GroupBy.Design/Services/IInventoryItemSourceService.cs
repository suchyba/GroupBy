using GroupBy.Design.DTO.InventoryItemSource;

namespace GroupBy.Design.Services
{
    public interface IInventoryItemSourceService : IAsyncService<InventoryItemSourceDTO, InventoryItemSourceDTO, InventoryItemSourceCreateDTO, InventoryItemSourceDTO>
    {

    }
}
