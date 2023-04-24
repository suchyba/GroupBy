namespace GroupBy.Design.DbContext
{
    public interface IDbContextLocator<Context> where Context : Microsoft.EntityFrameworkCore.DbContext
    {
        Context GetDbContext();
        void SetDbContext(Context dbContext);
        void RemoveDbContext();
    }
}
