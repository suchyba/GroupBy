using FluentValidation;
using GroupBy.Design.DTO.Agreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Agreement
{
    public class AgreementValidator : AbstractValidator<AgreementDTO>
    {
        public AgreementValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(a => a.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
