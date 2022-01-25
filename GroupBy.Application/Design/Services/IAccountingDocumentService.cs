using GroupBy.Application.DTO.AccountingDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IAccountingDocumentService : IAsyncService<AccountingDocumentDTO, AccountingDocumentDTO, AccountingDocumentCreateDTO, AccountingDocumentDTO>
    {

    }
}
