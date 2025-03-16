using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class AccountingBookTemplateRepository : AsyncRepository<AccountingBookTemplate>, IAccountingBookTemplateRepository
    {
        public AccountingBookTemplateRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<AccountingBookTemplate, bool>> CompareKeys(object entity)
        {
            return a => entity.GetType().GetProperty("Id").GetValue(entity).Equals(a.Id);
        }
    }
}
