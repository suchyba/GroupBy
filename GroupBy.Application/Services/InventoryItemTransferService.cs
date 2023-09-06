using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.DTO.InventoryItemTransfer;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryItemTransferService : AsyncService<InventoryItemTransfer, InventoryItemTransferSimpleDTO, InventoryItemTransferDTO, InventoryItemTransferCreateDTO, InventoryItemTransferUpdateDTO>, IInventoryItemTransferService
    {
        private readonly IInventoryBookRecordRepository inventoryBookRecordRepository;
        private readonly IInventoryItemSourceRepository inventoryItemSourceRepository;
        private readonly IDocumentRepository documentRepository;
        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IInventoryBookRepository inventoryBookRepository;
        private readonly IValidator<InventoryItemTransferConfirmDTO> confirmValidator;

        public InventoryItemTransferService(
            IInventoryItemTransferRepository repository,
            IInventoryBookRecordRepository inventoryBookRecordRepository,
            IInventoryItemSourceRepository inventoryItemSourceRepository,
            IDocumentRepository documentRepository,
            IInventoryItemRepository inventoryItemRepository,
            IGroupRepository groupRepository,
            IInventoryBookRepository inventoryBookRepository,
            IMapper mapper,
            IValidator<InventoryItemTransferUpdateDTO> updateValidator,
            IValidator<InventoryItemTransferCreateDTO> createValidator,
            IValidator<InventoryItemTransferConfirmDTO> confirmValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.inventoryBookRecordRepository = inventoryBookRecordRepository;
            this.inventoryItemSourceRepository = inventoryItemSourceRepository;
            this.documentRepository = documentRepository;
            this.inventoryItemRepository = inventoryItemRepository;
            this.groupRepository = groupRepository;
            this.inventoryBookRepository = inventoryBookRepository;
            this.confirmValidator = confirmValidator;
        }

        public async Task<InventoryItemTransferDTO> ConfirmTransferAsync(InventoryItemTransferConfirmDTO model)
        {
            var validationResult = await confirmValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var transfer = await repository.GetAsync(new { Id = model.Id }, includes: new string[]
                {
                    "SourceInventoryBook",
                    "DestinationInventoryBook.RelatedGroup",
                    "OutcomeInventoryBookRecord.Item",
                    "OutcomeInventoryBookRecord.Document",
                    "IncomeInventoryBookRecord"
                });

                var source = await inventoryItemSourceRepository.GetAsync(new { Id = model.InventoryItemSourceId });

                var document = await documentRepository.GetAsync(new { Id = transfer.OutcomeInventoryBookRecord.Document.Id }, includes: "Groups");

                var documentGroups = document.Groups.ToList();
                documentGroups.Add(transfer.DestinationInventoryBook.RelatedGroup);
                document.Groups = documentGroups;

                await documentRepository.UpdateAsync(document);

                var incomeRecord = await inventoryBookRecordRepository.CreateAsync(new InventoryBookRecord
                {
                    Book = transfer.DestinationInventoryBook,
                    Date = transfer.OutcomeInventoryBookRecord.Date,
                    Document = transfer.OutcomeInventoryBookRecord.Document,
                    Income = true,
                    Item = transfer.OutcomeInventoryBookRecord.Item,
                    Source = source
                });

                transfer.ConfirmationDate = model.ConfirmationDateTime;
                transfer.IncomeInventoryBookRecord = incomeRecord;

                var updatedTransfer = await repository.UpdateAsync(transfer);
                await uow.Commit();
                return mapper.Map<InventoryItemTransferDTO>(updatedTransfer);
            }
        }

        protected override async Task<InventoryItemTransfer> CreateOperationAsync(InventoryItemTransferCreateDTO model)
        {
            var entity = mapper.Map<InventoryItemTransfer>(model);
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                entity.SourceInventoryBook = await inventoryBookRepository.GetAsync(new { Id = entity.SourceInventoryBook.Id });
                entity.DestinationInventoryBook = await inventoryBookRepository.GetAsync(new { Id = entity.DestinationInventoryBook.Id });

                if (entity.SourceInventoryBook == entity.DestinationInventoryBook)
                    throw new BadRequestException("Source and destination inventory books must be different");

                var document = await documentRepository.GetAsync(new { Id = model.DocumentId }, includes: "Groups");

                var sourceInventoryBook = await inventoryBookRepository.GetAsync(new { Id = model.SourceInventoryBookId }, includes: "RelatedGroup", asTracking: false);

                if (!document.Groups.Any(g => g.Id == sourceInventoryBook.RelatedGroup.Id))
                    throw new BadRequestException($"Document not exists in source group");

                var item = await inventoryItemRepository.GetAsync(new { Id = model.InventoryItemId });
                if (!(await inventoryBookRepository.GetInventoryItemsAsync(sourceInventoryBook)).Contains(item))
                    throw new BadRequestException("Item does not exist in source inventory book");

                var source = await inventoryItemSourceRepository.GetAsync(new { Id = model.SourceInventoryItemSourceId });

                var ourcomeRecord = await inventoryBookRecordRepository.CreateAsync(new InventoryBookRecord
                {
                    Book = entity.SourceInventoryBook,
                    Date = model.TransferDate,
                    Document = document,
                    Income = false,
                    Item = item,
                    Source = source
                });

                var transfer = await repository.CreateAsync(new InventoryItemTransfer
                {
                    SourceInventoryBook = entity.SourceInventoryBook,
                    DestinationInventoryBook = entity.DestinationInventoryBook,
                    CreationDate = model.CreationDate,
                    OutcomeInventoryBookRecord = ourcomeRecord
                });
                await uow.Commit();
                return transfer;
            }
        }

        public override async Task<IEnumerable<InventoryItemTransferSimpleDTO>> GetAllAsync(bool includeLocal = false)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<InventoryItemTransferSimpleDTO>>(await repository.GetAllAsync(includes: new string[]
                    {
                        "SourceInventoryBook",
                        "DestinationInventoryBook",
                        "OutcomeInventoryBookRecord",
                        "IncomeInventoryBookRecord"
                    }, includeLocal: includeLocal));
            }
        }

        public override async Task<InventoryItemTransferDTO> GetAsync(InventoryItemTransferSimpleDTO model)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<InventoryItemTransferDTO>(await repository.GetAsync(mapper.Map<InventoryItemTransfer>(model), includes: new string[]
                    {
                        "SourceInventoryBook",
                        "DestinationInventoryBook",
                        "OutcomeInventoryBookRecord",
                        "IncomeInventoryBookRecord"
                    }));
            }
        }

        protected async override Task<InventoryItemTransfer> UpdateOperationAsync(InventoryItemTransfer entity)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var transferToUpdate = await repository.GetAsync(new { Id = entity.Id }, includes: new string[]
                {
                    "SourceInventoryBook",
                    "DestinationInventoryBook",
                    "OutcomeInventoryBookRecord",
                    "IncomeInventoryBookRecord"
                });

                if (transferToUpdate.ConfirmedByReceiver)
                    throw new BadRequestException("Transfer is already confirmed");

                var destInvBook = await inventoryBookRepository.GetAsync(new { Id = entity.DestinationInventoryBook.Id });

                if (destInvBook == transferToUpdate.SourceInventoryBook)
                    throw new BadRequestException("Source and destination inventory books must be different");

                transferToUpdate.DestinationInventoryBook = destInvBook;

                var updatedTransfer = await repository.UpdateAsync(transferToUpdate);
                await uow.Commit();
                return updatedTransfer;
            }
        }

        public async Task CancelTransferAsync(Guid id)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var transfer = await repository.GetAsync(new { Id = id }, includes: new string[]
                {
                    "SourceInventoryBook",
                    "DestinationInventoryBook",
                    "OutcomeInventoryBookRecord",
                    "IncomeInventoryBookRecord"
                });

                if (transfer.ConfirmedByReceiver)
                    throw new BadRequestException("Transfer is already confirmed");

                await inventoryBookRecordRepository.DeleteAsync(transfer.OutcomeInventoryBookRecord);
                await repository.DeleteAsync(transfer);
                await uow.Commit();
            }
        }
    }
}
