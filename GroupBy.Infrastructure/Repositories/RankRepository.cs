using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GroupBy.Data.Repositories
{
    public class RankRepository : AsyncRepository<Rank>, IRankRepository
    {
        public RankRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<Rank, bool>> CompareKeys(object entity)
        {
            return r => entity.GetType().GetProperty("Id").GetValue(entity).Equals(r.Id);
        }
    }
}
