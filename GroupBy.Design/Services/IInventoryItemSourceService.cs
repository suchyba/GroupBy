using GroupBy.Design.TO.InventoryItemSource;

namespace GroupBy.Design.Services
{
    public interface IInventoryItemSourceService : IAsyncService<InventoryItemSourceDTO, InventoryItemSourceDTO, InventoryItemSourceCreateDTO, InventoryItemSourceDTO>
    {

    }
}
