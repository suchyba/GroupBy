using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.ViewModels.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Rank
{
    public class RankCreateValidator : AbstractValidator<RankCreateViewModel>
    {
        public RankCreateValidator(IRankRepository rankRepository)
        {
            RuleFor(r => r.HigherRankId)
                .MustAsync(async (id, cancelation) =>
                {
                    return id != (await rankRepository.GetMaxIdAsync()) + 1;
                }).When(r => r.HigherRankId != null).WithMessage("{PropertyName} must be other rank.");
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
