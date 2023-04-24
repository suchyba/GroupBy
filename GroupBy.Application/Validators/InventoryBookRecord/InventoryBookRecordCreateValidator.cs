using FluentValidation;
using GroupBy.Design.TO.InventoryBookRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryBookRecord
{
    public class InventoryBookRecordCreateValidator : AbstractValidator<InventoryBookRecordCreateDTO>
    {
        public InventoryBookRecordCreateValidator()
        {
            RuleFor(r => r.InventoryBookId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.DocumentId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.ItemId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
