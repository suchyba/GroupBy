using GroupBy.Application.ViewModels.Agreement;

namespace GroupBy.Application.Design.Services
{
    public interface IAgreementService : IAsyncService<AgreementViewModel, AgreementCreateViewModel, AgreementViewModel>
    {

    }
}
