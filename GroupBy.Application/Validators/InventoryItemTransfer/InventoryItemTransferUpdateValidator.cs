using FluentValidation;
using GroupBy.Design.DTO.InventoryItemTransfer;

namespace GroupBy.Application.Validators.InventoryItemTransfer
{
    public class InventoryItemTransferUpdateValidator : AbstractValidator<InventoryItemTransferUpdateDTO>
    {
        public InventoryItemTransferUpdateValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(r => r.DestinationInventoryBookId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
