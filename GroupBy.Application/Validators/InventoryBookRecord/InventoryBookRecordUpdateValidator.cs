﻿using FluentValidation;
using GroupBy.Design.DTO.InventoryBookRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryBookRecord
{
    public class InventoryBookRecordUpdateValidator : AbstractValidator<InventoryBookRecordUpdateDTO>
    {
        public InventoryBookRecordUpdateValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
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
