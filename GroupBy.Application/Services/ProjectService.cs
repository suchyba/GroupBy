using AutoMapper;
using FluentValidation;
using GroupBy.Data.DbContexts;
using GroupBy.Design.Repositories;
using GroupBy.Design.Services;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.FinancialRecord;
using GroupBy.Design.TO.Project;
using GroupBy.Design.UnitOfWork;
using GroupBy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class ProjectService : AsyncService<Project, ProjectSimpleDTO, ProjectDTO, ProjectCreateDTO, ProjectUpdateDTO>, IProjectService
    {
        public ProjectService(
            IProjectRepository repository,
            IMapper mapper,
            IValidator<ProjectUpdateDTO> updateValidator,
            IValidator<ProjectCreateDTO> createValidator,
            IUnitOfWorkFactory<GroupByDbContext> unitOfWorkFactory)
            : base(repository, mapper, updateValidator, createValidator, unitOfWorkFactory)
        {

        }

        public async Task<IEnumerable<AccountingDocumentSimpleDTO>> GetRelatedAccountingDocumentsAsync(ProjectSimpleDTO project)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<AccountingDocumentSimpleDTO>>((await (repository as IProjectRepository).GetRelatedAccountingDocumentsAsync(mapper.Map<Project>(project))));
            }
        }

        public async Task<IEnumerable<FinancialRecordSimpleDTO>> GetRelatedFinancialRecordsAsync(ProjectSimpleDTO project)
        {
            using (var uow = unitOfWorkFactory.CreateUnitOfWork())
            {
                return mapper.Map<IEnumerable<FinancialRecordSimpleDTO>>((await repository.GetAsync(mapper.Map<Project>(project))).RelatedFinnancialRecords);
            }
        }
    }
}
