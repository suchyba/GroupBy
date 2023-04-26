using FluentValidation;
using GroupBy.Design.DTO.FinancialOutcomeRecord;
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
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
