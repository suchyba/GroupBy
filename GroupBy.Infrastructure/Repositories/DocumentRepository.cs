using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class DocumentRepository : AsyncRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<Document, bool>> CompareKeys(object entity)
        {
            return d => entity.GetType().GetProperty("Id").GetValue(entity).Equals(d.Id);
        }
    }
}
