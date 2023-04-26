using FluentValidation;
using GroupBy.Design.Repositories;
using GroupBy.Design.DTO.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Rank
{
    public class RankCreateValidator : AbstractValidator<RankCreateDTO>
    {
        public RankCreateValidator(IRankRepository rankRepository)
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
