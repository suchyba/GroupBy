using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.InventoryBookRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryBookRecordService : AsyncService<InventoryBookRecord, InventoryBookRecordSimpleDTO, InventoryBookRecordSimpleDTO, InventoryBookRecordCreateDTO, InventoryBookRecordUpdateDTO>, IInventoryBookRecordService
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly IInventoryItemSourceRepository inventoryItemSourceRepository;
        private readonly IInventoryBookRepository inventoryBookRepository;
        private readonly IValidator<InventoryBookRecordTransferDTO> transferValidator;

        public InventoryBookRecordService(
            IInventoryBookRecordRepository repository,
            IDocumentRepository documentRepository,
            IInventoryItemRepository inventoryItemRepository,
            IInventoryItemSourceRepository inventoryItemSourceRepository,
            IInventoryBookRepository inventoryBookRepository,
            IMapper mapper,
            IValidator<InventoryBookRecordUpdateDTO> updateValidator,
            IValidator<InventoryBookRecordCreateDTO> createValidator,
            IValidator<InventoryBookRecordTransferDTO> transferValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.documentRepository = documentRepository;
            this.inventoryItemRepository = inventoryItemRepository;
            this.inventoryItemSourceRepository = inventoryItemSourceRepository;
            this.inventoryBookRepository = inventoryBookRepository;
            this.transferValidator = transferValidator;
        }

        public override async Task<InventoryBookRecordSimpleDTO> GetAsync(InventoryBookRecordSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<InventoryBookRecordSimpleDTO>(await repository.GetAsync(mapper.Map<InventoryBookRecord>(model), includes: "Book"));
            }
        }

        protected override async Task<InventoryBookRecord> CreateOperationAsync(InventoryBookRecord entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Book = await inventoryBookRepository.GetAsync(entity.Book, includes: "Records.Item");
                entity.Item = await inventoryItemRepository.GetAsync(entity.Item);

                if (!entity.Income && entity.Book.Records.OrderBy(r => r.Date)
                                                         .Where(r => r.Item == entity.Item)
                                                         .LastOrDefault()?.Income != true)
                    throw new BadRequestException("You cannot remove item what is not in the inventory book");

                entity.Source = await inventoryItemSourceRepository.GetAsync(entity.Source);
                entity.Document = await documentRepository.GetAsync(entity.Document);

                var createdRecord = await repository.CreateAsync(entity);
                await uow.Commit();

                return createdRecord;
            }
        }

        protected override async Task<InventoryBookRecord> UpdateOperationAsync(InventoryBookRecord entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.Document = await documentRepository.GetAsync(entity.Document);
                entity.Item = await inventoryItemRepository.GetAsync(entity.Item);
                entity.Source = await inventoryItemSourceRepository.GetAsync(entity.Source);
                entity.Book = await inventoryBookRepository.GetAsync(entity.Book);

                var updatedRecord = await repository.UpdateAsync(entity);
                await uow.Commit();
                return updatedRecord;
            }
        }

        public async Task<IEnumerable<InventoryBookRecordDTO>> TransferItemAsync(InventoryBookRecordTransferDTO record)
        {
            var validationResult = await transferValidator.ValidateAsync(record);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var bookFrom = await inventoryBookRepository.GetAsync(new { Id = record.InventoryBookFromId }, includes: new string[] { "Records.Item", "RelatedGroup" });
                var bookTo = await inventoryBookRepository.GetAsync(new { Id = record.InventoryBookToId }, includes: new string[] { "Records.Item", "RelatedGroup" });
                var item = await inventoryItemRepository.GetAsync(new { Id = record.ItemId });

                if (bookFrom.Records.OrderBy(r => r.Date)
                                    .Where(r => r.Item == item)
                                    .LastOrDefault()?.Income != true)
                    throw new BadRequestException("You cannot remove item what is not in the inventory book");


                var sourceFrom = await inventoryItemSourceRepository.GetAsync(new { Id = record.SourceFromId });
                var sourceTo = await inventoryItemSourceRepository.GetAsync(new { Id = record.SourceToId });

                var document = await documentRepository.CreateAsync(new Document
                {
                    Name = record.DocumentName,
                    FilePath = "",
                    Groups = new List<Group>() { bookFrom.RelatedGroup, bookTo.RelatedGroup },
                    RelatedProject = null
                });

                if (document == null)
                    throw new BadRequestException($"Cannot create document {document.Name}");

                var recordFrom = await repository.CreateAsync(new InventoryBookRecord
                {
                    Book = bookFrom,
                    Item = item,
                    Income = false,
                    Source = sourceFrom,
                    Date = record.Date,
                    Document = document
                });

                var recordTo = await repository.CreateAsync(new InventoryBookRecord
                {
                    Book = bookTo,
                    Item = item,
                    Income = true,
                    Source = sourceTo,
                    Date = record.Date,
                    Document = document
                });

                await uow.Commit();
                return mapper.Map<IEnumerable<InventoryBookRecordDTO>>(new List<InventoryBookRecord>() { recordFrom, recordTo});
            }
        }

    }
}
