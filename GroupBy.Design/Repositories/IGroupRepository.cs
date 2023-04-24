using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IGroupRepository : IAsyncRepository<Group>
    {
        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(Guid group, bool includeLocal = false);
        public Task<IEnumerable<Group>> GetSubgroupsAsync(Guid parentGroupId, bool includeLocal = false);
        public Task AddMemberAsync(Guid groupId, Volunteer volunteer, bool includeLocal = false);
        public Task RemoveMemberAsync(Guid groupId, Volunteer volunteer);
        public Task<bool> IsMember(Guid groupId, Volunteer volunteer);
        public Task<IEnumerable<Project>> GetProjectsAsync(Guid groupId, bool includeLocal = false);
        public Task<IEnumerable<AccountingDocument>> GetAccountingDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false);
        public Task<IEnumerable<Document>> GetDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false);
    }
}
