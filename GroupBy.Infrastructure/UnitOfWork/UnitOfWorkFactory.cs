using GroupBy.Design.DbContext;
using GroupBy.Design.UnitOfWork;
using GroupBy.Data.DbContexts;

namespace GroupBy.Data.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory<GroupByDbContext>
    {
        private readonly IDbContextFactory<GroupByDbContext> dbContextFactory;
        private readonly IDbContextLocator<GroupByDbContext> dbContextLocator;
        private readonly IUnitOfWorkLocator<UnitOfWork> unitOfWorkLocator;

        public UnitOfWorkFactory(
            IDbContextFactory<GroupByDbContext> dbContextFactory,
            IDbContextLocator<GroupByDbContext> dbContextLocator,
            IUnitOfWorkLocator<UnitOfWork> unitOfWorkLocator)
        {
            this.dbContextFactory = dbContextFactory;
            this.dbContextLocator = dbContextLocator;
            this.unitOfWorkLocator = unitOfWorkLocator;
        }
        public IUnitOfWork<GroupByDbContext> CreateUnitOfWork()
        {
            return new UnitOfWork(dbContextLocator, dbContextFactory, unitOfWorkLocator);
        }
    }
}
