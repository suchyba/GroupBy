using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ProjectRepository : AsyncRepository<Project>, IProjectRepository
    {
        private readonly IGroupRepository groupRepository;
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IProjectRepository projectRepository;

        public ProjectRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IGroupRepository groupRepository,
            IVolunteerRepository volunteerRepository,
            IProjectRepository projectRepository) : base(dBcontextLocator)
        {
            this.groupRepository = groupRepository;
            this.volunteerRepository = volunteerRepository;
            this.projectRepository = projectRepository;
        }

        public override async Task<Project> CreateAsync(Project domain)
        {
            Guid projectOwnerId = domain.Owner.Id;
            domain.Owner = await volunteerRepository.GetAsync(domain.Owner);
            if (domain.Owner == null)
                throw new NotFoundException("Volunteer", projectOwnerId);

            Guid parentGroupId = domain.ParentGroup.Id;
            domain.ParentGroup = await groupRepository.GetAsync(domain.ParentGroup, false, "Members");
            if (domain.ParentGroup == null)
                throw new NotFoundException("Group", parentGroupId);

            EntityEntry<Project> project = null;

            if (domain.Independent)
            {
                var projectGroup = await groupRepository.CreateAsync(new Group
                {
                    Description = $"This is a group of {domain.Name} project",
                    Name = domain.Name + " group",
                    ParentGroup = domain.ParentGroup,
                    Owner = domain.Owner
                });

                domain.ProjectGroup = projectGroup;
            }
            else
                domain.ProjectGroup = null;

            project = await context.Set<Project>().AddAsync(domain);

            return project.Entity;
        }

        public async Task<IEnumerable<AccountingDocument>> GetRelatedAccountingDocumentsAsync(Project domain, bool includeLocal = false)
        {
            Project project = await projectRepository.GetAsync(domain, includeLocal, "RelatedElements");

            if (project == null)
                throw new NotFoundException("Project", domain.Id);

            List<AccountingDocument> documents = project.RelatedElements
                .Where(e => e is AccountingDocument)
                .Select(e => (AccountingDocument)e)
                .ToList();

            return documents;
        }

        public override async Task<Project> UpdateAsync(Project domain)
        {
            Project project = await GetAsync(domain, false, "Owner", "ParentGroup", "ProjectGroup");
            project.Active = domain.Active;
            project.BeginDate = domain.BeginDate;
            project.Description = domain.Description;
            project.EndDate = domain.EndDate;
            project.Independent = domain.Independent;
            project.Name = domain.Name;

            Guid projectOwnerId = domain.Owner.Id;
            project.Owner = await volunteerRepository.GetAsync(domain.Owner);
            if (project.Owner == null)
                throw new NotFoundException("Volunteer", projectOwnerId);

            Guid parentGroupId = domain.ParentGroup.Id;
            project.ParentGroup = await groupRepository.GetAsync(domain.ParentGroup, false, "Members");
            if (project.ParentGroup == null)
                throw new NotFoundException("Group", parentGroupId);

            if (domain.ProjectGroup != null)
            {
                Guid projectGroupId = domain.ProjectGroup.Id;
                project.ProjectGroup = await groupRepository.GetAsync(domain.ProjectGroup);
                if (project.ProjectGroup == null)
                    throw new NotFoundException("Group", projectGroupId);
            }

            return project;
        }
    }
}
