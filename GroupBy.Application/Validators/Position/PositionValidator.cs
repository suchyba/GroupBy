using FluentValidation;
using GroupBy.Application.ViewModels.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Position
{
    public class PositionValidator : AbstractValidator<PositionViewModel>
    {
        public PositionValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.HigherPositionId)
                .GreaterThan(0).When(p => p.HigherPositionId != null).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
