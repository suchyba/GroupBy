using FluentValidation;
using GroupBy.Application.DTO.InventoryBookRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryBookRecord
{
    public class InventoryBookRecordTransferValidator : AbstractValidator<InventoryBookRecordTransferDTO>
    {
        public InventoryBookRecordTransferValidator()
        {
            RuleFor(r => r.InventoryBookFromId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.InventoryBookToId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.DocumentName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.ItemId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceFromId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceToId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
