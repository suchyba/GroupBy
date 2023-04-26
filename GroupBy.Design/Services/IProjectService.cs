using GroupBy.Design.DTO.AccountingDocument;
using GroupBy.Design.DTO.FinancialRecord;
using GroupBy.Design.DTO.Project;

namespace GroupBy.Design.Services
{
    public interface IProjectService : IAsyncService<ProjectSimpleDTO, ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>
    {
        public Task<IEnumerable<FinancialRecordSimpleDTO>> GetRelatedFinancialRecordsAsync(ProjectSimpleDTO project);
        Task<IEnumerable<AccountingDocumentSimpleDTO>> GetRelatedAccountingDocumentsAsync(ProjectSimpleDTO projectSimpleDTO);
    }
}
