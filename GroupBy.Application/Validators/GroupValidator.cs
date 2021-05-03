using FluentValidation;
using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators
{
    public class GroupValidator : AbstractValidator<GroupViewModel>
    {
        public GroupValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Group name can't be empty.");
        }
    }
}
