using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Volunteer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IVolunteerService : IAsyncService<VolunteerDTO, VolunteerCreateDTO, VolunteerUpdateDTO>
    {
        public Task<IEnumerable<GroupDTO>> GetGroupsAsync(int volunteerId);
    }
}
