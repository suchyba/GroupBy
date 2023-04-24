namespace GroupBy.Design.DbContext
{
    public interface IDbContextFactory<Context> where Context : Microsoft.EntityFrameworkCore.DbContext
    {
        Context CreateDbContext();
    }
}
