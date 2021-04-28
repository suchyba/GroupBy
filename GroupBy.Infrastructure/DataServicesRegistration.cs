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
            services.AddDbContext<GroupByDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GroupByLocalConnectionString")));

            services.AddScoped<IVolunteerAsyncRepository, VolunteerDevelopmentRepository>();
            services.AddScoped<IGroupAsyncRepository, GroupDevelopmentRepository>();
            services.AddScoped<IAccountingBookAsyncRepository, AccountingBookDevelopmentRepository>();
            services.AddScoped<IAccountingBookAsyncRepository, AccountingBookDevelopmentRepository>();

            return services;
        }
    }
}
