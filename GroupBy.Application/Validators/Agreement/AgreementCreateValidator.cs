using FluentValidation;
using GroupBy.Application.DTO.Agreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Agreement
{
    public class AgreementCreateValidator : AbstractValidator<AgreementCreateDTO>
    {
        public AgreementCreateValidator()
        {
            RuleFor(a => a.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
