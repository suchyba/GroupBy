using FluentValidation;
using GroupBy.Application.DTO.InventoryBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.InventoryBook
{
    public class InventoryBookValidator : AbstractValidator<InventoryBookDTO>
    {
        public InventoryBookValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
