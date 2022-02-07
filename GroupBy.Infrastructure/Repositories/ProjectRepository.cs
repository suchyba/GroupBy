using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Exceptions;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ProjectRepository : AsyncRepository<Project>, IProjectRepository
    {
        private readonly IGroupRepository groupRepository;

        public ProjectRepository(DbContext context, IGroupRepository groupRepository) : base(context)
        {
            this.groupRepository = groupRepository;
        }

        public override async Task<Project> CreateAsync(Project domain)
        {
            int projectOwnerId = domain.Owner.Id;
            domain.Owner = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == projectOwnerId);
            if (domain.Owner == null)
                throw new NotFoundException("Volunteer", projectOwnerId);

            int parentGroupId = domain.ParentGroup.Id;
            domain.ParentGroup = await context.Set<Group>().Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == parentGroupId);
            if (domain.ParentGroup == null)
                throw new NotFoundException("Group", parentGroupId);

            EntityEntry<Project> project = null;

            using (var transaction = context.Database.BeginTransaction())
            {
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

                await context.SaveChangesAsync();
                transaction.Commit();
            }
            return project.Entity;
        }

        public override async Task<Project> GetAsync(Project domain)
        {
            var project = await context.Set<Project>()
                .Include(p => p.RelatedFinnancialRecords)
                .Include(p => p.ParentGroup)
                .Include(p => p.ProjectGroup)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == domain.Id);
            if (project == null)
                throw new NotFoundException("Project", domain.Id);
            return project;
        }

        public override async Task<Project> UpdateAsync(Project domain)
        {
            Project project = await GetAsync(domain);
            project.Active = domain.Active;
            project.BeginDate = domain.BeginDate;
            project.Description = domain.Description;
            project.EndDate = domain.EndDate;
            project.Independent = domain.Independent;
            project.Name = domain.Name;

            int projectOwnerId = domain.Owner.Id;
            project.Owner = await context.Set<Volunteer>().FirstOrDefaultAsync(v => v.Id == projectOwnerId);
            if (project.Owner == null)
                throw new NotFoundException("Volunteer", projectOwnerId);

            int parentGroupId = domain.ParentGroup.Id;
            project.ParentGroup = await context.Set<Group>().Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == parentGroupId);
            if (project.ParentGroup == null)
                throw new NotFoundException("Group", parentGroupId);

            if (domain.ProjectGroup != null)
            {
                int projectGroupId = domain.ProjectGroup.Id;
                project.ProjectGroup = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == projectGroupId);
                if (project.ProjectGroup == null)
                    throw new NotFoundException("Group", projectGroupId);
            }

            await context.SaveChangesAsync();
            return project;
        }
    }
}
