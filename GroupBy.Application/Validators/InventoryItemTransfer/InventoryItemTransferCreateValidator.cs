using FluentValidation;
using GroupBy.Design.DTO.InventoryItemTransfer;
using System;

namespace GroupBy.Application.Validators.InventoryItemTransfer
{
    public class InventoryItemTransferCreateValidator : AbstractValidator<InventoryItemTransferCreateDTO>
    {
        public InventoryItemTransferCreateValidator()
        {
            RuleFor(r => r.CreationDate)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceInventoryBookId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.DestinationInventoryBookId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.SourceInventoryItemSourceId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.DocumentId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.InventoryItemId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.TransferDate)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required.");
        }
    }
}
