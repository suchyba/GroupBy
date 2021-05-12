using FluentValidation;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators
{
    public class GroupUpdateValidator : AbstractValidator<GroupUpdateViewModel>
    {
        public GroupUpdateValidator()
        {
            RuleFor(g => g.Id)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("{PropertyName} is reqired.");
            RuleFor(g => g.OwnerId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
