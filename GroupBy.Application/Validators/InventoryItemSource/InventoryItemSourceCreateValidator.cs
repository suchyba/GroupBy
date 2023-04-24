using FluentValidation;
using GroupBy.Design.TO.InventoryItemSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryItemSource
{
    public class InventoryItemSourceCreateValidator : AbstractValidator<InventoryItemSourceCreateDTO>
    {
        public InventoryItemSourceCreateValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
