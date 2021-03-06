using GroupBy.Application.DTO.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IPositionService : IAsyncService<PositionSimpleDTO, PositionDTO, PositionCreateDTO, PositionSimpleDTO>
    {

    }
}
