using FluentValidation;
using GroupBy.Application.DTO.InventoryItemSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryItemSource
{
    public class InventoryItemSourceValidator : AbstractValidator<InventoryItemSourceDTO>
    {
        public InventoryItemSourceValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
