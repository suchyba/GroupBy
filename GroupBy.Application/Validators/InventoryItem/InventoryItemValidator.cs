using FluentValidation;
using GroupBy.Application.DTO.InventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryItem
{
    public class InventoryItemValidator : AbstractValidator<InventoryItemSimpleDTO>
    {
        public InventoryItemValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Symbol)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Value)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
