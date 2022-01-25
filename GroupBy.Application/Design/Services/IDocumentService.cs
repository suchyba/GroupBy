using GroupBy.Application.DTO.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IDocumentService : IAsyncService<DocumentDTO, DocumentDTO, DocumentCreateDTO, DocumentUpdateDTO>
    {

    }
}
