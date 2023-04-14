using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.InventoryBook;
using GroupBy.Design.TO.InventoryBookRecord;
using GroupBy.Design.TO.InventoryItem;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryBookService : AsyncService<InventoryBook, InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>, IInventoryBookService
    {
        public InventoryBookService(
            IInventoryBookRepository repository,
            IMapper mapper,
            IValidator<InventoryBookUpdateDTO> updateValidator,
            IValidator<InventoryBookCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        public async Task<IEnumerable<InventoryBookRecordListDTO>> GetInventoryBookRecordsAsync(InventoryBookSimpleDTO inventoryBookDTO)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<InventoryBookRecordListDTO>>(await (repository as IInventoryBookRepository).GetInventoryBookRecordsAsync(mapper.Map<InventoryBook>(inventoryBookDTO)));
            }
        }

        public async Task<IEnumerable<InventoryItemSimpleDTO>> GetInventoryItemsAsync(InventoryBookSimpleDTO inventoryBookDTO)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<InventoryItemSimpleDTO>>(await (repository as IInventoryBookRepository).GetInventoryItemsAsync(mapper.Map<InventoryBook>(inventoryBookDTO)));
            }
        }
    }
}
