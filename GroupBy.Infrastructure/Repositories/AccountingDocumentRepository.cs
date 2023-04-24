using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class AccountingDocumentRepository : AsyncRepository<AccountingDocument>, IAccountingDocumentRepository
    {
        public AccountingDocumentRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<AccountingDocument, bool>> CompareKeys(object entity)
        {
            return d => entity.GetType().GetProperty("Id").GetValue(entity).Equals(d.Id);
        }
    }
}
