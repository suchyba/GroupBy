using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Volunteer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IGroupService : IAsyncService<GroupSimpleDTO, GroupDTO, GroupCreateDTO, GroupUpdateDTO>
    {
        public Task<IEnumerable<VolunteerSimpleDTO>> GetVolunteersAsync(int groupId);
        public Task AddMemberAsync(int groupId, int volunteerId);
        public Task RemoveMemberAsync(int groupId, int volunteerId);
        public Task<IEnumerable<GroupSimpleDTO>> GetSubgroupsAsync(int groupId);
        public Task<IEnumerable<AccountingBookSimpleDTO>> GetAccountingBooksAsync(int groupId);
    }
}
