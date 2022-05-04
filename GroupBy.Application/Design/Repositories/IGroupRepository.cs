using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Repositories
{
    public interface IGroupRepository : IAsyncRepository<Group>
    {
        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(int group);
        public Task<IEnumerable<Group>> GetSubgroupsAsync(int parentGroupId);
        public Task AddMemberAsync(int groupId, int volunteerId);
        public Task RemoveMemberAsync(int groupId, int volunteerId);
        public Task<bool> IsMember(int groupId, int volunteerId);
        public Task<IEnumerable<Project>> GetProjectsAsync(int groupId);
        public Task<IEnumerable<AccountingDocument>> GetAccountingDocumentsAsync(int groupId);
    }
}
