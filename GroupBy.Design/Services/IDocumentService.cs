using GroupBy.Design.DTO.Document;

namespace GroupBy.Design.Services
{
    public interface IDocumentService : IAsyncService<DocumentDTO, DocumentDTO, DocumentCreateDTO, DocumentUpdateDTO>
    {

    }
}
