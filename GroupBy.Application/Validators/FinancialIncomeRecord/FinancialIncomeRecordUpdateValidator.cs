using FluentValidation;
using GroupBy.Design.DTO.FinancialIncomeRecord;
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
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
