using GroupBy.Application.DTO.InventoryItemSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IInventoryItemSourceService : IAsyncService<InventoryItemSourceDTO, InventoryItemSourceDTO, InventoryItemSourceCreateDTO, InventoryItemSourceDTO>
    {

    }
}
