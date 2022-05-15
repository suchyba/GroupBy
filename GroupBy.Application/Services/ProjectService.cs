using AutoMapper;
using FluentValidation;
using GroupBy.Application.Design.Repositories;
using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.AccountingDocument;
using GroupBy.Application.DTO.FinancialRecord;
using GroupBy.Application.DTO.Project;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class ProjectService : AsyncService<Project, ProjectSimpleDTO, ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>, IProjectService
    {
        public ProjectService(IProjectRepository repository, IMapper mapper, 
            IValidator<ProjectUpdateDTO> updateValidator, IValidator<ProjectCreateDTO> createValidator) 
            : base(repository, mapper, updateValidator, createValidator)
        {

        }

        public async Task<IEnumerable<AccountingDocumentSimpleDTO>> GetRelatedAccountingDocumentsAsync(ProjectSimpleDTO project)
        {
            return mapper.Map<IEnumerable<AccountingDocumentSimpleDTO>>((await (repository as IProjectRepository).GetRelatedAccountingDocumentsAsync(mapper.Map<Project>(project))));
        }

        public async Task<IEnumerable<FinancialRecordSimpleDTO>> GetRelatedFinancialRecordsAsync(ProjectSimpleDTO project)
        {
            return mapper.Map<IEnumerable<FinancialRecordSimpleDTO>>((await repository.GetAsync(mapper.Map<Project>(project))).RelatedFinnancialRecords);
        }
    }
}
