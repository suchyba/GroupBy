using FluentValidation;
using GroupBy.Design.DTO.InventoryItemSource;
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
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
