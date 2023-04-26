using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.DTO.Project;
using GroupBy.Design.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Project
{
    public class ProjectCreateValidator : AbstractValidator<ProjectCreateDTO>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory;

        public ProjectCreateValidator(
            IGroupRepository groupRepository,
            IVolunteerRepository volunteerRepository,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
        {
            this.groupRepository = groupRepository;
            this.volunteerRepository = volunteerRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
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
                .WithMessage("Owner must be a member of the parent group.");

            RuleFor(p => p.ParentGroupId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
        private async Task<bool> OwnerInParentGroup(ProjectCreateDTO project, CancellationToken token)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                var volunteer = await volunteerRepository.GetAsync(new Domain.Entities.Volunteer { Id = project.OwnerId });
                return await groupRepository.IsMember(project.ParentGroupId, volunteer);
            }
        }
    }
}
