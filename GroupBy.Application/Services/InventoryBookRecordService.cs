using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.InventoryBookRecord;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class InventoryBookRecordService : AsyncService<InventoryBookRecord, InventoryBookRecordSimpleDTO, InventoryBookRecordSimpleDTO, InventoryBookRecordCreateDTO, InventoryBookRecordUpdateDTO>, IInventoryBookRecordService
    {
        private readonly IValidator<InventoryBookRecordTransferDTO> transferValidator;

        public InventoryBookRecordService(
            IInventoryBookRecordRepository repository,
            IMapper mapper,
            IValidator<InventoryBookRecordUpdateDTO> updateValidator,
            IValidator<InventoryBookRecordCreateDTO> createValidator,
            IValidator<InventoryBookRecordTransferDTO> transferValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {
            this.transferValidator = transferValidator;
        }

        public async Task<IEnumerable<InventoryBookRecordDTO>> TransferItemAsync(InventoryBookRecordTransferDTO record)
        {
            // TODO refactor to move complex logic from repository to service
            var validationResult = await transferValidator.ValidateAsync(record);
            if (!validationResult.IsValid)
                throw new Design.Exceptions.ValidationException(validationResult);

            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                IEnumerable<InventoryBookRecord> transferedRecords = await (repository as IInventoryBookRecordRepository).TransferItemAsync(
                        new InventoryBookRecord
                        {
                            Book = new InventoryBook { Id = record.InventoryBookFromId },
                            Item = new InventoryItem { Id = record.ItemId },
                            Income = false,
                            Source = new InventoryItemSource { Id = record.SourceFromId },
                            Date = record.Date,
                            Document = new Document { Name = record.DocumentName }
                        },
                        new InventoryBookRecord
                        {
                            Book = new InventoryBook { Id = record.InventoryBookToId },
                            Item = new InventoryItem { Id = record.ItemId },
                            Income = true,
                            Source = new InventoryItemSource { Id = record.SourceToId },
                            Date = record.Date,
                            Document = new Document { Name = record.DocumentName }
                        });
                await uow.Commit();
                return mapper.Map<IEnumerable<InventoryBookRecordDTO>>(transferedRecords);
            }
        }

    }
}
