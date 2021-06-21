using FluentValidation;
using GroupBy.Application.DTO.InventoryBookRecord;
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
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.Document)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.ItemId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
