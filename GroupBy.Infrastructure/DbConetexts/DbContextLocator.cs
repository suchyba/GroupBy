using GroupBy.Design.DbContext;
using GroupBy.Data.DbContexts;
using System;

namespace GroupBy.Data.DbConetexts
{
    public class DbContextLocator : IDbContextLocator<GroupByDbContext>
    {
        private GroupByDbContext dbContext;

        private readonly object lockObject = new object();

        public GroupByDbContext GetDbContext() => dbContext;

        public void RemoveDbContext()
        {
            lock (lockObject)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }

        public void SetDbContext(GroupByDbContext context)
        {
            if (context is GroupByDbContext)
            {
                lock (lockObject)
                {
                    if (dbContext == null)
                    {
                        dbContext = context;
                    }
                }
            }
        }
    }
}
