using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels.InventoryItem;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryItemService : AsyncService<InventoryItem, InventoryItemViewModel, InventoryItemCreateViewModel, InventoryItemViewModel>, IInventoryItemService
    {
        public InventoryItemService(IInventoryItemRepository inventoryItemRepository, IMapper mapper, 
            IValidator<InventoryItemViewModel> updateValidator, IValidator<InventoryItemCreateViewModel> createValidator) 
            : base(inventoryItemRepository, mapper, updateValidator, createValidator)
        {

        }
    }
}
