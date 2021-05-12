using FluentValidation;
using GroupBy.Application.Design.Services;
using GroupBy.Application.Services;
using GroupBy.Application.Validators;
using GroupBy.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            services.AddScoped <IValidator<VolunteerViewModel>, VolunteerValidator>();
            services.AddScoped <IValidator<VolunteerCreateViewModel>, VolunteerCreateValidator>();
            services.AddScoped <IValidator<VolunteerUpdateViewModel>, VolunteerUpdateValidator>();

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAccountingBookService, AccountingBookService>();
            services.AddScoped<IVolunteerService, VolunteerService>();

            return services;
        }
    }
}
