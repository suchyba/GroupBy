using FluentValidation;
using GroupBy.Design.DTO.InventoryItemTransfer;
using System;

namespace GroupBy.Application.Validators.InventoryItemTransfer
{
    public class InventoryItemTransferConfirmValidator : AbstractValidator<InventoryItemTransferConfirmDTO>
    {
        public InventoryItemTransferConfirmValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(t => t.InventoryItemSourceId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(t => t.ConfirmationDateTime)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} needs to be smaller or equal to today.");
        }
    }
}
