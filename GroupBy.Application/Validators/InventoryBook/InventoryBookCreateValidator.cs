using FluentValidation;
using GroupBy.Design.DTO.InventoryBook;
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
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
