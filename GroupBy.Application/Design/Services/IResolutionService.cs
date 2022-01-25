using GroupBy.Application.DTO.Resolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IResolutionService : IAsyncService<ResolutionDTO, ResolutionDTO, ResolutionCreateDTO, ResolutionUpdateDTO>
    {

    }
}
