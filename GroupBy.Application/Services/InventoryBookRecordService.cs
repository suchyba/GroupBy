using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryBookRecord;
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
            IValidator<InventoryBookRecordTransferDTO> transferValidator)
            : base(repository, mapper, updateValidator, createValidator)
        {
            this.transferValidator = transferValidator;
        }

        public async Task<IEnumerable<InventoryBookRecordDTO>> TransferItemAsync(InventoryBookRecordTransferDTO record)
        {
            var validationResult = await transferValidator.ValidateAsync(record);
            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            return mapper.Map<IEnumerable<InventoryBookRecordDTO>>((repository as IInventoryBookRecordRepository).TransferItemAsync(new InventoryBookRecord
            {
                Book = new InventoryBook { Id = record.InventoryBookFromId },
                Item = new InventoryItem { Id = record.ItemId },
                Income = false,
                InventoryBookId = record.InventoryBookFromId,
                Source = new InventoryItemSource { Id = record.SourceFromId },
                Date = record.Date,
                Document = new Document { Id = record.DocumentId }
            },
            new InventoryBookRecord
            {
                Book = new InventoryBook { Id = record.InventoryBookToId },
                Item = new InventoryItem { Id = record.ItemId },
                Income = true,
                InventoryBookId = record.InventoryBookToId,
                Source = new InventoryItemSource { Id = record.SourceToId },
                Date = record.Date,
                Document = new Document { Id = record.DocumentId }
            }));
        }

    }
}
