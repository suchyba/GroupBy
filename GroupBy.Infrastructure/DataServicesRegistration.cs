using GroupBy.Application.Design.Repositories;
using GroupBy.Data.DbContexts;
using GroupBy.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GroupBy.Data
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, GroupByDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GroupByLocalConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<GroupByDbContext>()
                .AddDefaultTokenProviders();

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
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IInventoryItemSourceRepository, InventoryItemSourceRepository>();
            services.AddScoped<IAccountingDocumentRepository, AccountingDocumentRepository>();
            services.AddScoped<IResolutionRepository, ResolutionRepository>();
            services.AddScoped<IFinancialOutcomeRecordRepository, FinancialOutcomeRecordRepository>();
            services.AddScoped<IFinancialIncomeRecordRepository, FinancialIncomeRecordRepository>();
            services.AddScoped<IRegistrationCodeRepository, RegistrationCodeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            return services;
        }
    }
}
