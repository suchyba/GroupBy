using FluentValidation;
using GroupBy.Design.TO.Resolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Resolution
{
    public class ResolutionCreateValidator : AbstractValidator<ResolutionCreateDTO>
    {
        public ResolutionCreateValidator()
        {
            RuleFor(r => r.Symbol)
                .NotEmpty().WithMessage("{PropeertyName} cannot be empty");
            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required");
            RuleFor(r => r.Content)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(r => r.LegislatorId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.GroupId)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
