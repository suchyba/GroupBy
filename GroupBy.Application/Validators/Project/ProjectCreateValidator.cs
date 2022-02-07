using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Project
{
    public class ProjectCreateValidator : AbstractValidator<ProjectCreateDTO>
    {
        private readonly IGroupRepository groupRepository;

        public ProjectCreateValidator(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.BeginDate)
                .GreaterThan(DateTime.MinValue)
                .When(p => p.BeginDate.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(p => p.EndDate)
                .When(p => p.BeginDate.HasValue && p.EndDate.HasValue)
                .WithMessage("{PropertyName} must be less or equal to ending date.");

            RuleFor(p => p.EndDate)
                .GreaterThan(DateTime.MinValue)
                .When(p => p.EndDate.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.")
                .GreaterThanOrEqualTo(p => p.BeginDate)
                .When(p => p.BeginDate.HasValue && p.EndDate.HasValue)
                .WithMessage("{PropertyName} must be greater or equal to begin date.");

            RuleFor(p => p.OwnerId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");

            RuleFor(p => p)
                .MustAsync(OwnerInParentGroup)
                .When(p => p.OwnerId > 0)
                .OverridePropertyName("OwnerId")
                .WithMessage("Owner must be a member of the parent group.");

            RuleFor(p => p.ParentGroupId)
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
        private async Task<bool> OwnerInParentGroup(ProjectCreateDTO project, CancellationToken token)
        {
            return await groupRepository.IsMember(project.ParentGroupId, project.OwnerId);
        }
    }
}
