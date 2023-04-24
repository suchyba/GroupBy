using Microsoft.EntityFrameworkCore.Storage;

namespace GroupBy.Design.UnitOfWork
{
    public interface IUnitOfWork<Context> : IDisposable where Context : Microsoft.EntityFrameworkCore.DbContext
    {
        bool IsDisposed { get; }
        Task Commit();
        IDbContextTransaction BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        Context DbContext { get; }
    }
}
