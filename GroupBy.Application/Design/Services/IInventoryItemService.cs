using GroupBy.Application.DTO.InventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IInventoryItemService : IAsyncService<InventoryItemDTO, InventoryItemCreateDTO, InventoryItemDTO>
    {

    }
}
