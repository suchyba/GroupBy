using GroupBy.Design.DTO.RegistrationCode;

namespace GroupBy.Design.Services
{
    public interface IRegistrationCodeService : IAsyncService<RegistrationCodeSimpleDTO, RegistrationCodeFullDTO, RegistrationCodeCreateDTO, RegistrationCodeUpdateDTO>
    {

    }
}
