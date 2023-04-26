using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.InventoryBook;
using GroupBy.Design.DTO.InventoryBookRecord;
using GroupBy.Design.DTO.InventoryItem;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryBookService : AsyncService<InventoryBook, InventoryBookSimpleDTO, InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>, IInventoryBookService
    {
        private readonly IGroupRepository groupRepository;

        public InventoryBookService(
            IInventoryBookRepository repository,
            IGroupRepository groupRepository,
            IMapper mapper,
            IValidator<InventoryBookUpdateDTO> updateValidator,
            IValidator<InventoryBookCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.groupRepository = groupRepository;
        }
        public override async Task<InventoryBookDTO> GetAsync(InventoryBookSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<InventoryBookDTO>(await repository.GetAsync(mapper.Map<InventoryBook>(model), includes: "RelatedGroup"));
            }
        }

        protected override async Task<InventoryBook> CreateOperationAsync(InventoryBook entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.RelatedGroup = await groupRepository.GetAsync(entity.RelatedGroup, includes: "InventoryBook");

                if (entity.RelatedGroup.InventoryBook != null)
                    throw new BadRequestException("Inventory book exists in this group");

                var createdBook = await repository.CreateAsync(entity);
                await uow.Commit();
                return createdBook;
            }
        }

        protected override async Task<InventoryBook> UpdateOperationAsync(InventoryBook entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.RelatedGroup = await groupRepository.GetAsync(entity.RelatedGroup, asTracking: false, includes: "InventoryBook");

                if (entity.RelatedGroup.InventoryBook != null && entity.RelatedGroup.InventoryBook.Id != entity.Id)
                    throw new BadRequestException("Inventory book exists in this group");

                var updatedBook = await repository.UpdateAsync(entity);
                await uow.Commit();
                return updatedBook;
            }
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
