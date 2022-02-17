using FluentValidation;
using GroupBy.Application.DTO.RegistrationCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.RegistrationCode
{
    public class RegistrationCodeCreateValidator : AbstractValidator<RegistrationCodeCreateDTO>
    {
        public RegistrationCodeCreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(c => c.TargetGroupId)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(c => c.OwnerId)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(c => c.TargetRankId)
                .GreaterThan(0)
                .When(c => c.TargetRankId.HasValue).WithMessage("{PropertyName} is required");
        }
    }
}
