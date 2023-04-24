﻿using FluentValidation;
using GroupBy.Design.TO.Group;

namespace GroupBy.Application.Validators.Group
{
    public class GroupValidator : AbstractValidator<GroupDTO>
    {
        public GroupValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Group name can't be empty.");
        }
    }
}
