using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.InventoryBookRecord;
using GroupBy.Design.DTO.InventoryItem;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryItemService : AsyncService<InventoryItem, InventoryItemSimpleDTO, InventoryItemSimpleDTO, InventoryItemCreateDTO, InventoryItemSimpleDTO>, IInventoryItemService
    {
        public InventoryItemService(
            IInventoryItemRepository inventoryItemRepository,
            IMapper mapper,
            IValidator<InventoryItemSimpleDTO> updateValidator,
            IValidator<InventoryItemCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(inventoryItemRepository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        public async Task<IEnumerable<InventoryBookRecordSimpleDTO>> GetInventoryItemHistoryAsync(Guid inventoryItemId)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<InventoryBookRecordSimpleDTO>>(await (repository as IInventoryItemRepository).GetInventoryItemHistoryAsync(inventoryItemId));
            }
        }

        public async Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsWithoutHistoryAsync()
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<InventoryItemSimpleDTO>>(await (repository as IInventoryItemRepository).GetInventoryItemWithoutHistory());
            }
        }
    }
}
