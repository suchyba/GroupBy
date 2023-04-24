using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Repositories;
using GroupBy.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupBy.Data.Repositories
{
    public class RegistrationCodeRepository : AsyncRepository<RegistrationCode>, IRegistrationCodeRepository
    {
        public RegistrationCodeRepository(
            IDbContextLocator<GroupByDbContext> dBcontextLocator) : base(dBcontextLocator)
        {

        }

        public override Task<RegistrationCode> UpdateAsync(RegistrationCode domain)
        {
            throw new BadRequestException("Operation not allowed");
        }

        protected override Expression<Func<RegistrationCode, bool>> CompareKeys(object entity)
        {
            return c => c.Code == (string)entity.GetType().GetProperty("Code").GetValue(entity);
        }
    }
}
