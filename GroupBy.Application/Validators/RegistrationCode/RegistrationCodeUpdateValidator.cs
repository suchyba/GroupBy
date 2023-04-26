using FluentValidation;
using GroupBy.Design.DTO.RegistrationCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.RegistrationCode
{
    public class RegistrationCodeUpdateValidator : AbstractValidator<RegistrationCodeUpdateDTO>
    {
        public RegistrationCodeUpdateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(c => c.TargetGroupId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(c => c.TargetRankId)
                .NotEmpty()
                .When(c => c.TargetRankId.HasValue).WithMessage("{PropertyName} is required");
        }
    }
}
