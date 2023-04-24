using FluentValidation;
using GroupBy.Design.TO.InventoryItem;
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
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Symbol)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Value)
                .NotEmpty().WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
