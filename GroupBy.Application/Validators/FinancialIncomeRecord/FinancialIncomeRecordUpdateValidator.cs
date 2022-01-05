using FluentValidation;
using GroupBy.Application.DTO.FinancialIncomeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.FinancialIncomeRecord
{
    public class FinancialIncomeRecordUpdateValidator : AbstractValidator<FinancialIncomeRecordUpdateDTO>
    {
        public FinancialIncomeRecordUpdateValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(r => r.RelatedProjectId)
                .GreaterThan(0).When(r => r.RelatedProjectId.HasValue).WithMessage("{PropertyName} have to be greater then 0");
        }
    }
}
