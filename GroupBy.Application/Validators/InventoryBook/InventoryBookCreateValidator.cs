using FluentValidation;
using GroupBy.Application.DTO.InventoryBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryBook
{
    public class InventoryBookCreateValidator : AbstractValidator<InventoryBookCreateDTO>
    {
        public InventoryBookCreateValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(b => b.RelatedGroupId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
