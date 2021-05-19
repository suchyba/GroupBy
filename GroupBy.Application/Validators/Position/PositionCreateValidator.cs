using FluentValidation;
using GroupBy.Application.ViewModels.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Position
{
    public class PositionCreateValidator : AbstractValidator<PositionCreateViewModel>
    {
        public PositionCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.HigherPositionId)
                .GreaterThan(0).When(p => p.HigherPositionId != null).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
