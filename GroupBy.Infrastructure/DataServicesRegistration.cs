using GroupBy.Application.Design.Repositories;
using GroupBy.Data.DbContexts;
using GroupBy.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GroupBy.Data
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, GroupByDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GroupByLocalConnectionString")));

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IAccountingBookRepository, AccountingBookRepository>();

            return services;
        }
    }
}
