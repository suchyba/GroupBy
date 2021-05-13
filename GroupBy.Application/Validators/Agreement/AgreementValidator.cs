using FluentValidation;
using GroupBy.Application.ViewModels.Agreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Agreement
{
    public class AgreementValidator : AbstractValidator<AgreementViewModel>
    {
        public AgreementValidator()
        {
            RuleFor(a => a.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(a => a.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
