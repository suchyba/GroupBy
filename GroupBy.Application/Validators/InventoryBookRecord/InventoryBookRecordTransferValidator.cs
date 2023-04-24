using FluentValidation;
using GroupBy.Design.TO.InventoryBookRecord;
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
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.InventoryBookToId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.DocumentName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.ItemId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceFromId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceToId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
