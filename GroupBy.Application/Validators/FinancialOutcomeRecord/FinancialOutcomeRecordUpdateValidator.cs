using FluentValidation;
using GroupBy.Application.DTO.FinancialOutcomeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.FinancialOutcomeRecord
{
    public class FinancialOutcomeRecordUpdateValidator : AbstractValidator<FinancialOutcomeRecordUpdateDTO>
    {
        public FinancialOutcomeRecordUpdateValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
        }
    }
}
