using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class FinancialOutcomeRecordRepository : AsyncRepository<FinancialOutcomeRecord>, IFinancialOutcomeRecordRepository
    {
        public FinancialOutcomeRecordRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<FinancialOutcomeRecord, bool>> CompareKeys(object entity)
        {
            return r => entity.GetType().GetProperty("Id").GetValue(entity).Equals(r.Id);
        }
    }
}
