using FluentValidation;
using GroupBy.Application.ViewModels.Group;

namespace GroupBy.Application.Validators.Group
{
    public class GroupCreateValidator : AbstractValidator<GroupCreateViewModel>
    {
        public GroupCreateValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("{PropertyName} is reqired.");
            RuleFor(g => g.OwnerId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(g => g.ProjectId)
                .GreaterThan(0).When(g => g.ProjectId != null).WithMessage("{PropertyName} has to be greater than 0.");
            RuleFor(g => g.ParentGroupId)
                .GreaterThan(0).When(g => g.ParentGroupId != null).WithMessage("{PropertyName} has to be greater than 0.");
        }
    }
}
