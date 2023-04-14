using GroupBy.Domain.Entities;

namespace GroupBy.Design.Repositories
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
        Task<IEnumerable<AccountingDocument>> GetRelatedAccountingDocumentsAsync(Project project, bool includeLocal = false);
    }
}
