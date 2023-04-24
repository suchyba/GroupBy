using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.Document;
using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Project;
using GroupBy.Design.TO.Volunteer;

namespace GroupBy.Design.Services
{
    public interface IGroupService : IAsyncService<GroupSimpleDTO, GroupDTO, GroupCreateDTO, GroupUpdateDTO>
    {
        public Task<IEnumerable<VolunteerSimpleDTO>> GetVolunteersAsync(Guid groupId);
        public Task AddMemberAsync(Guid groupId, Guid volunteerId);
        public Task RemoveMemberAsync(Guid groupId, Guid volunteerId);
        public Task<IEnumerable<GroupSimpleDTO>> GetSubgroupsAsync(Guid groupId);
        public Task<IEnumerable<AccountingBookSimpleDTO>> GetAccountingBooksAsync(Guid groupId);
        public Task<IEnumerable<ProjectSimpleDTO>> GetProjectsAsync(Guid groupId);
        public Task<IEnumerable<AccountingDocumentSimpleDTO>> GetAccountingDocumentsAsync(Guid groupId, Guid? projectId);
        public Task<IEnumerable<DocumentDTO>> GetDocumentsAsync(Guid groupId, Guid? projectId);
    }
}
