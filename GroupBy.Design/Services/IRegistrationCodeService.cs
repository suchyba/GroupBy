using GroupBy.Design.TO.RegistrationCode;

namespace GroupBy.Design.Services
{
    public interface IRegistrationCodeService : IAsyncService<RegistrationCodeSimpleDTO, RegistrationCodeFullDTO, RegistrationCodeCreateDTO, RegistrationCodeUpdateDTO>
    {

    }
}
