using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Application.DTO.FinancialRecord;
using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IProjectService : IAsyncService<ProjectSimpleDTO, ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>
    {
        public Task<IEnumerable<FinancialRecordSimpleDTO>> GetRelatedFinancialRecordsAsync(ProjectSimpleDTO project);
        Task<IEnumerable<AccountingDocumentSimpleDTO>> GetRelatedAccountingDocumentsAsync(ProjectSimpleDTO projectSimpleDTO);
    }
}
