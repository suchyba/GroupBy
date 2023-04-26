using FluentValidation;
using GroupBy.Design.DTO.FinancialOutcomeRecord;

namespace GroupBy.Application.Validators.FinancialOutcomeRecord
{
    public class FinancialOutcomeRecordCreateValidator : AbstractValidator<FinancialOutcomeRecordCreateDTO>
    {
        public FinancialOutcomeRecordCreateValidator()
        {
            RuleFor(r => r.BookId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedDocumentId)
                .NotEmpty().WithMessage("{PropertyName} have to be greater then 0");
            RuleFor(r => r.RelatedProjectId)
                .NotEmpty().When(r => r.RelatedProjectId.HasValue).WithMessage("{PropertyName} have to be greater then 0");
        }
    }
}
