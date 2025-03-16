using GroupBy.Design.DTO.AccountingBookTemplate;

namespace GroupBy.Design.Services
{
    public interface IAccountingBookTemplateService : IAsyncService<AccountingBookTemplateSimpleDTO, AccountingBookTemplateDTO, AccountingBookTemplateCreateDTO, AccountingBookTemplateUpdateDTO>
    {

    }
}
