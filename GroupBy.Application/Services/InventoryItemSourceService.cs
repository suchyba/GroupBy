using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryItemSource;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryItemSourceService : AsyncService<InventoryItemSource, InventoryItemSourceDTO, InventoryItemSourceCreateDTO, InventoryItemSourceDTO>, IInventoryItemSourceService
    {
        public InventoryItemSourceService(
            IInventoryItemSourceRepository repository, 
            IMapper mapper, 
            IValidator<InventoryItemSourceDTO> updateValidator, 
            IValidator<InventoryItemSourceCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }
    }
}
