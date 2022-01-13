using FluentValidation;
using GroupBy.Application.DTO.Resolution;
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
                .GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(r => r.GroupId)
                .GreaterThan(0).WithMessage("{PropertyName} is required");
        }
    }
}
