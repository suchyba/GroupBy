using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class GroupRepository : AsyncRepository<Group>, IGroupRepository
    {
        public GroupRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task AddMemberAsync(Guid groupId, Volunteer volunteer, bool includeLocal = false)
        {
            var group = await GetAsync(new Group { Id = groupId }, includeLocal, includes: "Members");

            if (group.Members == null)
                group.Members = new List<Volunteer>();

            group.Members.Add(volunteer);
        }

        public async Task<IEnumerable<AccountingDocument>> GetAccountingDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false)
        {
            Group group = await GetAsync(new Group { Id = groupId }, includeLocal, includes: new string[] { "Elements.RelatedProject", "ProjectsRealisedInGroup", "RelatedProject" });

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
            Group group = await GetAsync(new Group { Id = groupId }, includeLocal, includes: new string[] { "Elements.RelatedProject", "ProjectsRealisedInGroup", "RelatedProject" });

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
            return (await GetAsync(new Group { Id = groupId }, includeLocal, includes: "ProjectsRealisedInGroup")).ProjectsRealisedInGroup;
        }

        public async Task<IEnumerable<Group>> GetSubgroupsAsync(Guid groupId, bool includeLocal = false)
        {
            return (await GetAsync(new Group { Id = groupId }, includeLocal, includes: "ChildGroups")).ChildGroups;
        }

        public async Task<IEnumerable<Volunteer>> GetVolunteersAsync(Guid group, bool includeLocal = false)
        {
            Group g = await GetAsync(new Group { Id = group }, includeLocal, includes: "Members");

            if (g.Members == null)
                return new List<Volunteer>();

            return g.Members.Where(v => v.Confirmed);
        }

        public async Task<bool> IsMember(Guid groupId, Volunteer volunteer)
        {
            var group = await GetAsync(new Group { Id = groupId }, false, includes: "Members");

            if (group.Members == null)
                return false;

            return group.Members.Any(m => m.Id == volunteer.Id);
        }

        public async Task RemoveMemberAsync(Guid groupId, Volunteer volunteer)
        {
            var group = await GetAsync(new Group { Id = groupId }, false, includes: "Members");

            group.Members.Remove(volunteer);
        }

        protected override Expression<Func<Group, bool>> CompareKeys(object entity)
        {
            return g => entity.GetType().GetProperty("Id").GetValue(entity).Equals(g.Id);
        }
    }
}
