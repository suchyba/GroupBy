namespace GroupBy.Design.UnitOfWork
{
    public interface IUnitOfWorkFactory<Context> where Context : Microsoft.EntityFrameworkCore.DbContext
    {
        IUnitOfWork<Context> CreateUnitOfWork();
    }
}
