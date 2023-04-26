using FluentValidation;
using GroupBy.Design.DTO.Resolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Resolution
{
    public class ResolutionUpdateValidator :AbstractValidator<ResolutionUpdateDTO>
    {
        public ResolutionUpdateValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(r => r.Symbol)
                .NotEmpty().WithMessage("{PropeertyName} cannot be empty");
            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} is required");
            RuleFor(r => r.Content)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
