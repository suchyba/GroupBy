using FluentValidation;
using GroupBy.Application.DTO.FinancialIncomeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.FinancialIncomeRecord
{
    public class FinancialIncomeRecordCreateValidator : AbstractValidator<FinancialIncomeRecordCreateDTO>
    {
        public FinancialIncomeRecordCreateValidator()
        {
            RuleFor(r => r.BookId)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(r => r.BookOrderNumberId)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedDocumentId)
                .GreaterThan(0).WithMessage("{PropertyName} have to be greater then 0");
            RuleFor(r => r.RelatedProjectId)
                .GreaterThan(0).When(r => r.RelatedProjectId.HasValue).WithMessage("{PropertyName} have to be greater then 0");
        }
    }
}
