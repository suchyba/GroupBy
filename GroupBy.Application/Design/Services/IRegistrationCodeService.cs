using GroupBy.Application.DTO.RegistrationCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IRegistrationCodeService : IAsyncService<RegistrationCodeSimpleDTO, RegistrationCodeFullDTO, RegistrationCodeCreateDTO, RegistrationCodeUpdateDTO>
    {

    }
}
