using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class ProjectRepository : AsyncRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public async Task<IEnumerable<AccountingDocument>> GetRelatedAccountingDocumentsAsync(Project domain, bool includeLocal = false)
        {
            Project project = await GetAsync(domain, includeLocal, includes: "RelatedElements");

            List<AccountingDocument> documents = project.RelatedElements
                .Where(e => e is AccountingDocument)
                .Select(e => (AccountingDocument)e)
                .ToList();

            return documents;
        }

        protected override Expression<Func<Project, bool>> CompareKeys(object entity)
        {
            return p => entity.GetType().GetProperty("Id").GetValue(entity).Equals(p.Id);
        }
    }
}
