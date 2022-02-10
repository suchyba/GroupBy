using FluentValidation;
using GroupBy.Application.DTO.Group;

namespace GroupBy.Application.Validators.Group
{
    public class GroupCreateValidator : AbstractValidator<GroupCreateDTO>
    {
        public GroupCreateValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("{PropertyName} is reqired.");
            RuleFor(g => g.OwnerId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(g => g.ParentGroupId)
                .GreaterThan(0).When(g => g.ParentGroupId != null).WithMessage("{PropertyName} has to be greater than 0.");
            RuleFor(g => g.ParentGroupId)
                .NotNull().WithMessage("You have to declare parent group!");
        }
    }
}
