using FluentValidation;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Services;
using GroupBy.Application.Validators.AccountingBook;
using GroupBy.Application.Validators.Agreement;
using GroupBy.Application.Validators.Group;
using GroupBy.Application.Validators.Position;
using GroupBy.Application.Validators.Rank;
using GroupBy.Application.Validators.Volunteer;
using GroupBy.Application.ViewModels.AccountingBook;
using GroupBy.Application.ViewModels.Agreement;
using GroupBy.Application.ViewModels.Group;
using GroupBy.Application.ViewModels.Position;
using GroupBy.Application.ViewModels.Rank;
using GroupBy.Application.ViewModels.Volunteer;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GroupBy.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IValidator<GroupViewModel>, GroupValidator>();
            services.AddScoped<IValidator<GroupCreateViewModel>, GroupCreateValidator>();
            services.AddScoped<IValidator<GroupUpdateViewModel>, GroupUpdateValidator>();

            services.AddScoped<IValidator<AccountingBookViewModel>, AccountingBookUpdateValidator>();
            services.AddScoped<IValidator<AccountingBookCreateViewModel>, AccountingBookCreateValidator>();

            services.AddScoped<IValidator<VolunteerViewModel>, VolunteerValidator>();
            services.AddScoped<IValidator<VolunteerCreateViewModel>, VolunteerCreateValidator>();
            services.AddScoped<IValidator<VolunteerUpdateViewModel>, VolunteerUpdateValidator>();

            services.AddScoped<IValidator<AgreementViewModel>, AgreementValidator>();
            services.AddScoped<IValidator<AgreementCreateViewModel>, AgreementCreateValidator>();

            services.AddScoped<IValidator<RankViewModel>, RankValidator>();
            services.AddScoped<IValidator<RankCreateViewModel>, RankCreateValidator>();

            services.AddScoped<IValidator<PositionViewModel>, PositionValidator>();
            services.AddScoped<IValidator<PositionCreateViewModel>, PositionCreateValidator>();

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAccountingBookService, AccountingBookService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<IPositionService, PositionService>();

            return services;
        }
    }
}
