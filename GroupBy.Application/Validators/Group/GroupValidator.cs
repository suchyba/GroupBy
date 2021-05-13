using FluentValidation;
using GroupBy.Application.ViewModels.Group;

namespace GroupBy.Application.Validators.Group
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
