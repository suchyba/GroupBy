using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Volunteer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IGroupService : IAsyncService<GroupDTO, GroupCreateDTO, GroupUpdateDTO>
    {
        public Task<IEnumerable<VolunteerSimpleDTO>> GetVolunteersAsync(int groupId);
        public Task AddMember(int groupId, int volunteerId);
    }
}
