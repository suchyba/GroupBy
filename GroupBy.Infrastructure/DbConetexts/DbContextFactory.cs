using GroupBy.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GroupBy.Data.DbConetexts
{
    public class DbContextFactory : Design.DbContext.IDbContextFactory<GroupByDbContext>
    {
        private readonly string connectionString;
        public DbContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public GroupByDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<GroupByDbContext> optionsBuilder = new DbContextOptionsBuilder<GroupByDbContext>();

            var dbContextOptions = optionsBuilder.UseSqlServer(connectionString);

            return new GroupByDbContext(dbContextOptions.Options);
        }
    }
}
