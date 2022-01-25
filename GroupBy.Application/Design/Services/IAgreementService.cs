using GroupBy.Application.DTO.Agreement;

namespace GroupBy.Application.Design.Services
{
    public interface IAgreementService : IAsyncService<AgreementDTO, AgreementDTO, AgreementCreateDTO, AgreementDTO>
    {

    }
}
