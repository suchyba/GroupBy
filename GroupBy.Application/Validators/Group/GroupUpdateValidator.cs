using FluentValidation;
using GroupBy.Design.TO.Group;

namespace GroupBy.Application.Validators.Group
{
    public class GroupUpdateValidator : AbstractValidator<GroupUpdateDTO>
    {
        public GroupUpdateValidator()
        {
            RuleFor(g => g.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("{PropertyName} is reqired.");
            RuleFor(g => g.OwnerId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
