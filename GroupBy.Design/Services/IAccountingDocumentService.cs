using GroupBy.Design.DTO.AccountingDocument;

namespace GroupBy.Design.Services
{
    public interface IAccountingDocumentService : IAsyncService<AccountingDocumentSimpleDTO, AccountingDocumentDTO, AccountingDocumentCreateDTO, AccountingDocumentSimpleDTO>
    {

    }
}
