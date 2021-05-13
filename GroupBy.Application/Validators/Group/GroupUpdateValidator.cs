using FluentValidation;
using GroupBy.Application.ViewModels.Group;

namespace GroupBy.Application.Validators.Group
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
