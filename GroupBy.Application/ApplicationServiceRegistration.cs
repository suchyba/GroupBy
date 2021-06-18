using FluentValidation;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Services;
using GroupBy.Application.Validators.AccountingBook;
using GroupBy.Application.Validators.Agreement;
using GroupBy.Application.Validators.Group;
using GroupBy.Application.Validators.InventoryItem;
using GroupBy.Application.Validators.Position;
using GroupBy.Application.Validators.Rank;
using GroupBy.Application.Validators.Volunteer;
using GroupBy.Application.DTO.AccountingBook;
using GroupBy.Application.DTO.Agreement;
using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Application.DTO.Position;
using GroupBy.Application.DTO.Rank;
using GroupBy.Application.DTO.Volunteer;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.Validators.InventoryBook;

namespace GroupBy.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            RegisterValidators(services);

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAccountingBookService, AccountingBookService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IInventoryItemService, InventoryItemService>();
            services.AddScoped<IInventoryBookService, InventoryBookService>();

            return services;
        }
        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<GroupDTO>, GroupValidator>();
            services.AddScoped<IValidator<GroupCreateDTO>, GroupCreateValidator>();
            services.AddScoped<IValidator<GroupUpdateDTO>, GroupUpdateValidator>();

            services.AddScoped<IValidator<AccountingBookDTO>, AccountingBookUpdateValidator>();
            services.AddScoped<IValidator<AccountingBookCreateDTO>, AccountingBookCreateValidator>();

            services.AddScoped<IValidator<VolunteerDTO>, VolunteerValidator>();
            services.AddScoped<IValidator<VolunteerCreateDTO>, VolunteerCreateValidator>();
            services.AddScoped<IValidator<VolunteerUpdateDTO>, VolunteerUpdateValidator>();

            services.AddScoped<IValidator<AgreementDTO>, AgreementValidator>();
            services.AddScoped<IValidator<AgreementCreateDTO>, AgreementCreateValidator>();

            services.AddScoped<IValidator<RankDTO>, RankValidator>();
            services.AddScoped<IValidator<RankCreateDTO>, RankCreateValidator>();

            services.AddScoped<IValidator<PositionDTO>, PositionValidator>();
            services.AddScoped<IValidator<PositionCreateDTO>, PositionCreateValidator>();

            services.AddScoped<IValidator<InventoryItemDTO>, InventoryItemValidator>();
            services.AddScoped<IValidator<InventoryItemCreateDTO>, InventoryItemCreateValidator>();

            services.AddScoped<IValidator<InventoryBookDTO>, InventoryBookValidator>();
            services.AddScoped<IValidator<InventoryBookCreateDTO>, InventoryBookCreateValidator>();
            services.AddScoped<IValidator<InventoryBookUpdateDTO>, InventoryBookUpdateValidator>();
        }
    }
}
