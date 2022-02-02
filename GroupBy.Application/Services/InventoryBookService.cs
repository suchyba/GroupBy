using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryBook;
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
    public class InventoryBookService : AsyncService<InventoryBook, InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>, IInventoryBookService
    {
        public InventoryBookService(IInventoryBookRepository repository, IMapper mapper, 
            IValidator<InventoryBookUpdateDTO> updateValidator, IValidator<InventoryBookCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }

        public async Task<IEnumerable<InventoryBookRecordListDTO>> GetInventoryBookRecordsAsync(InventoryBookSimpleDTO inventoryBookDTO)
        {
            return mapper.Map<IEnumerable<InventoryBookRecordListDTO>>(await (repository as IInventoryBookRepository).GetInventoryBookRecordsAsync(mapper.Map<InventoryBook>(inventoryBookDTO)));
        }

        public async Task<IEnumerable<InventoryItemDTO>> GetInventoryItemsAsync(InventoryBookSimpleDTO inventoryBookDTO)
        {
            return mapper.Map<IEnumerable<InventoryItemDTO>>(await (repository as IInventoryBookRepository).GetInventoryItemsAsync(mapper.Map<InventoryBook>(inventoryBookDTO)));
        }
    }
}
