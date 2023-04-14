using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IGroupRepository : IAsyncRepository<Group>
    {
        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(Guid group, bool includeLocal = false);
        public Task<IEnumerable<Group>> GetSubgroupsAsync(Guid parentGroupId, bool includeLocal = false);
        public Task AddMemberAsync(Guid groupId, Guid volunteerId, bool includeLocal = false);
        public Task RemoveMemberAsync(Guid groupId, Guid volunteerId);
        public Task<bool> IsMember(Guid groupId, Guid volunteerId);
        public Task<IEnumerable<Project>> GetProjectsAsync(Guid groupId, bool includeLocal = false);
        public Task<IEnumerable<AccountingDocument>> GetAccountingDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false);
        public Task<IEnumerable<Document>> GetDocumentsAsync(Guid groupId, Guid? projectId, bool includeLocal = false);
    }
}
