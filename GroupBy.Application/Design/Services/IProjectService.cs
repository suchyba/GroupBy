using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IProjectService : IAsyncService<ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>
    {
    }
}
