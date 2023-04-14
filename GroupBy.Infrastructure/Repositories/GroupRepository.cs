using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class GroupRepository : AsyncRepository<Group>, IGroupRepository
    {
        private readonly IVolunteerRepository volunteerRepository;

        public GroupRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator,
            IVolunteerRepository volunteerRepository) : base(dBcontextLocator)
        {
            this.volunteerRepository = volunteerRepository;
        }

        public async Task AddMemberAsync(Guid groupId, Guid volunteerId, bool includeLocal = false)
        {
            var group = await GetAsync(new Group { Id = groupId }, includeLocal, "Members", "RelatedProject");
            if (group == null)
                throw new NotFoundException("Group", groupId);

            var volunteer = await volunteerRepository.GetAsync(new Volunteer { Id = volunteerId }, includeLocal);
            if (volunteer == null)
                throw new NotFoundException("Volunteer", volunteer);

            if (group.RelatedProject != null && !group.RelatedProject.Active)
            {
                throw new BadRequestException("Cannot add member to the inactive project");
            }

            group.Members.Add(volunteer);
        }

        public override async Task<Group> CreateAsync(Group domain)
        {
            Guid ownerId = domain.Owner.Id;
            domain.Owner = await volunteerRepository.GetAsync(domain.Owner);
            if (domain.Owner == null)
                throw new NotFoundException("Volunteer", ownerId);

            if (domain.ParentGroup != null)
            {
                Guid parentGroupId = domain.ParentGroup.Id;
                domain.ParentGroup = await context.Set<Group>().FirstOrDefaultAsync(g => g.Id == parentGroupId);
                domain.ParentGroup = await GetAsync(domain.ParentGroup);
                if (domain.ParentGroup == null)
                    throw new NotFoundException("Group", parentGroupId);
            }
            var createdGroup = await context.Set<Group>().AddAsync(domain);

            return createdGroup.Entity;
        }

        public async Task<IEnumerable<AccountingDocument>> GetAccountingDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false)
        {
            Group group = await GetAsync(new Group { Id = groupId }, includeLocal, "Elements.RelatedProject", "ProjectsRealisedInGroup", "RelatedProject");
            if (group == null)
                throw new NotFoundException("Group", groupId);

            List<AccountingDocument> documents = group.Elements
                .Where(e => e is AccountingDocument)
                .Select(e => (AccountingDocument)e)
                .ToList();

            if (projectId.HasValue)
            {
                if (group.ProjectsRealisedInGroup
                    .Select(p => p.Id)
                    .Contains(projectId.Value)
                    || group.RelatedProject.Id == projectId)
                {
                    documents = documents
                        .Where(d => d.RelatedProject == null || d.RelatedProject.Id == projectId)
                        .ToList();
                }
                else
                {
                    throw new NotFoundException("Project", projectId.Value);
                }
            }
            return documents;
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false)
        {
            Group group = await GetAsync(new Group { Id = groupId }, includeLocal, "Elements.RelatedProject", "ProjectsRealisedInGroup", "RelatedProject");
            if (group == null)
                throw new NotFoundException("Group", groupId);

            List<Document> documents = group.Elements
                .Where(e => e is Document)
                .Select(e => (Document)e)
                .ToList();

            if (projectId.HasValue)
            {
                if (group.ProjectsRealisedInGroup
                    .Select(p => p.Id)
                    .Contains(projectId.Value)
                    || group.RelatedProject.Id == projectId)
                {
                    documents = documents
                        .Where(d => d.RelatedProject == null || d.RelatedProject.Id == projectId)
                        .ToList();
                }
                else
                {
                    throw new NotFoundException("Project", projectId.Value);
                }
            }
            return documents;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(Guid groupId, bool includeLocal = false)
        {
            return (await GetAsync(new Group { Id = groupId }, includeLocal, "ProjectsRealisedInGroup")).ProjectsRealisedInGroup;
        }

        public async Task<IEnumerable<Group>> GetSubgroupsAsync(Guid groupId, bool includeLocal = false)
        {
            return (await GetAsync(new Group { Id = groupId }, includeLocal, "ChildGroups")).ChildGroups;
        }

        public async Task<IEnumerable<Volunteer>> GetVolunteersAsync(Guid group, bool includeLocal = false)
        {
            Group g = await GetAsync(new Group { Id = group }, includeLocal, "Members");
            if (g == null)
                throw new NotFoundException("Group", group);

            return g.Members.Where(v => v.Confirmed);
        }

        public async Task<bool> IsMember(Guid groupId, Guid volunteerId)
        {
            var group = await GetAsync(new Group { Id = groupId }, false, "Members");

            var volunteer = await volunteerRepository.GetAsync(new Volunteer { Id = volunteerId });
            return group.Members.Contains(volunteer);
        }

        public async Task RemoveMemberAsync(Guid groupId, Guid volunteerId)
        {
            if (!await IsMember(groupId, volunteerId))
                throw new BadRequestException("Volunteer is not a member of the group");

            var group = await GetAsync(new Group { Id = groupId }, false, "Members");
            var volunteer = await volunteerRepository.GetAsync(new Volunteer { Id = volunteerId });

            if (group.Owner == volunteer)
                throw new BadRequestException("Volunteer cannot be an owner of the group");

            group.Members.Remove(volunteer);
        }

        public override async Task<Group> UpdateAsync(Group domain)
        {
            Group g = await GetAsync(domain);
            if (g == null)
                throw new NotFoundException("Group", domain.Id);

            Guid ownerId = domain.Owner.Id;
            domain.Owner = await volunteerRepository.GetAsync(domain.Owner);
            if (domain.Owner == null)
                throw new NotFoundException("Volunteer", ownerId);

            g.Name = domain.Name;
            g.Description = domain.Description;
            g.Owner = domain.Owner;
            return g;
        }
    }
}
