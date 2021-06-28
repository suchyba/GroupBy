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
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IAgreementRepository, AgreementRepository>();
            services.AddScoped<IRankRepository, RankRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
            services.AddScoped<IInventoryBookRepository, InventoryBookRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IInventoryBookRecordRepository, InventoryBookRecordRepository>();

            return services;
        }
    }
}
