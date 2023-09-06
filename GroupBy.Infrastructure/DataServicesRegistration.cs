using GroupBy.Data.DbConetexts;
using GroupBy.Data.DbContexts;
using GroupBy.Data.Repositories;
using GroupBy.Data.UnitOfWork;
using GroupBy.Design.DbContext;
using GroupBy.Design.Repositories;
using GroupBy.Design.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GroupBy.Data
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Only for generating database model
            /*services.AddDbContext<DbContext, GroupByDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GroupByLocalConnectionString")));*/
            services.AddSingleton<Design.DbContext.IDbContextFactory<GroupByDbContext>, DbContextFactory>(options =>
            {
                return new DbContextFactory(configuration.GetConnectionString("GroupByLocalConnectionString"));
            });
            services.AddScoped<IDbContextLocator<GroupByDbContext>, DbContextLocator>();
            services.AddScoped<IUnitOfWorkFactory<GroupByDbContext>, UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWorkLocator<UnitOfWork.UnitOfWork>, UnitOfWorkLocator>();

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
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IInventoryItemTransferRepository, InventoryItemTransferRepository>();

            services.AddScoped<IUserRoleRepository<Guid>, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository<Guid>, UserClaimRepository>();
            services.AddScoped<IUserLoginRepository<Guid>, UserLoginRepository>();
            services.AddScoped<IUserTokenRepository<Guid>, UserTokenRepository>();
            services.AddScoped<IRoleRepository<Guid>, RoleRepository>();
            services.AddScoped<IRoleClaimRepository<Guid>, RoleClaimRepository>();

            return services;
        }
    }
}
