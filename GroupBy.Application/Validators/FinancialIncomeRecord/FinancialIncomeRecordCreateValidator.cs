using FluentValidation;
using GroupBy.Design.DTO.FinancialIncomeRecord;

namespace GroupBy.Application.Validators.FinancialIncomeRecord
{
    public class FinancialIncomeRecordCreateValidator : AbstractValidator<FinancialIncomeRecordCreateDTO>
    {
        public FinancialIncomeRecordCreateValidator()
        {
            RuleFor(r => r.BookId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedDocumentId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedProjectId)
                .NotEmpty()
                .When(r => r.RelatedProjectId.HasValue)
                .WithMessage("{PropertyName} cannot be empty");
        }
    }
}
