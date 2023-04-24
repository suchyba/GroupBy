using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.FinancialRecord;
using GroupBy.Design.TO.Project;

namespace GroupBy.Design.Services
{
    public interface IProjectService : IAsyncService<ProjectSimpleDTO, ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>
    {
        public Task<IEnumerable<FinancialRecordSimpleDTO>> GetRelatedFinancialRecordsAsync(ProjectSimpleDTO project);
        Task<IEnumerable<AccountingDocumentSimpleDTO>> GetRelatedAccountingDocumentsAsync(ProjectSimpleDTO projectSimpleDTO);
    }
}
