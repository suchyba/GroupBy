using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryItemService : AsyncService<InventoryItem, InventoryItemSimpleDTO, InventoryItemSimpleDTO, InventoryItemCreateDTO, InventoryItemSimpleDTO>, IInventoryItemService
    {
        public InventoryItemService(IInventoryItemRepository inventoryItemRepository, IMapper mapper, 
            IValidator<InventoryItemSimpleDTO> updateValidator, IValidator<InventoryItemCreateDTO> createValidator) 
            : base(inventoryItemRepository, mapper, updateValidator, createValidator)
        {

        }

        public async Task<IEnumerable<InventoryBookRecordSimpleDTO>> GetInventoryItemHistoryAsync(int inventoryItemId)
        {
            return mapper.Map<IEnumerable<InventoryBookRecordSimpleDTO>>(await (repository as IInventoryItemRepository).GetInventoryItemHistoryAsync(inventoryItemId));
        }
    }
}
