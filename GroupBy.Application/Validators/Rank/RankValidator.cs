using FluentValidation;
using GroupBy.Application.DTO.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Rank
{
    public class RankValidator : AbstractValidator<RankDTO>
    {
        public RankValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(r => r.HigherRankId)
                .NotEqual(r => r.Id).When(r => r.HigherRankId != null).WithMessage("{PropertyName} must be other rank.");
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
