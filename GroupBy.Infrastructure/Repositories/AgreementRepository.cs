using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class AgreementRepository : AsyncRepository<Agreement>, IAgreementRepository
    {
        public AgreementRepository(IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }
        public override async Task<Agreement> UpdateAsync(Agreement domain)
        {
            var toModify = await GetAsync(domain);

            toModify.Content = domain.Content;

            return toModify;
        }

        protected override Expression<Func<Agreement, bool>> CompareKeys(object entity)
        {
            return a => entity.GetType().GetProperty("Id").GetValue(entity).Equals(a.Id);
        }
    }
}
