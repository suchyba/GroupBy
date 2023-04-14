using GroupBy.Design.DbContext;
using GroupBy.Design.UnitOfWork;
using GroupBy.Data.DbContexts;

namespace GroupBy.Data.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory<GroupByDbContext>
    {
        private readonly IDbContextFactory<GroupByDbContext> dbContextFactory;
        private readonly IDbContextLocator<GroupByDbContext> dbContextLocator;

        public UnitOfWorkFactory(IDbContextFactory<GroupByDbContext> dbContextFactory, IDbContextLocator<GroupByDbContext> dbContextLocator)
        {
            this.dbContextFactory = dbContextFactory;
            this.dbContextLocator = dbContextLocator;
        }
        public IUnitOfWork<GroupByDbContext> CreateUnitOfWork()
        {
            return new UnitOfWork(dbContextLocator, dbContextFactory);
        }
    }
}
