using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.TO.Project;
using GroupBy.Design.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Project
{
    public class ProjectUpdateValidator : AbstractValidator<ProjectUpdateDTO>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public ProjectUpdateValidator(IGroupRepository groupRepository, IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.groupRepository = groupRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");

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
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p)
                .MustAsync(OwnerInParentGroup)
                .When(p => p.OwnerId != Guid.Empty)
                .OverridePropertyName("OwnerId")
                .WithMessage("Owner must be a member of the parent group.")
                .MustAsync(OwnerInProjectGroup)
                .When(p => p.ProjectGroupId.HasValue && p.OwnerId != Guid.Empty)
                .WithMessage("Owner must be a member of the project group");

            RuleFor(p => p.ParentGroupId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ProjectGroupId)
                .NotEmpty()
                .When(p => p.ProjectGroupId.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.")
                .NotNull()
                .When(p => p.Independent)
                .WithMessage("{PropertyName} is required.")
                .NotEqual(p => p.ParentGroupId)
                .When(p => p.ProjectGroupId.HasValue)
                .WithMessage("Project and parent groups cannot be the same ones.");


        }
        private async Task<bool> OwnerInParentGroup(ProjectUpdateDTO project, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return await groupRepository.IsMember(project.ParentGroupId, new Domain.Entities.Volunteer { Id = project.OwnerId });
            }
        }
        private async Task<bool> OwnerInProjectGroup(ProjectUpdateDTO project, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return await groupRepository.IsMember(project.ProjectGroupId.Value, new Domain.Entities.Volunteer { Id = project.OwnerId });
            }
        }
    }
}
