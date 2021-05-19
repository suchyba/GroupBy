using FluentValidation;
using GroupBy.Application.ViewModels.InventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryItem
{
    public class InventoryItemCreateValidator : AbstractValidator<InventoryItemCreateViewModel>
    {
        public InventoryItemCreateValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Symbol)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(i => i.Value)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
