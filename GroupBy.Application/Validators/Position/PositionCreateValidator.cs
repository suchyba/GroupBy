using FluentValidation;
using GroupBy.Design.DTO.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Position
{
    public class PositionCreateValidator : AbstractValidator<PositionCreateDTO>
    {
        public PositionCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.HigherPositionId)
                .NotEmpty().When(p => p.HigherPositionId != null).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
