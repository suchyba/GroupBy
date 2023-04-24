using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class PositionRepository : AsyncRepository<Position>, IPositionRepository
    {
        public PositionRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        protected override Expression<Func<Position, bool>> CompareKeys(object entity)
        {
            return p => entity.GetType().GetProperty("Id").GetValue(entity).Equals(p.Id);
        }
    }
}
