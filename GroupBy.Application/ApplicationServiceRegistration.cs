using GroupBy.Application.Design.Services;
using GroupBy.Application.Services;
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

            services.AddScoped<IGroupAsyncService, GroupAsyncService>();
            //services.AddScoped<IVolunteerAsyncService, VolunteerAsyncService>();
            services.AddScoped<IAccountingBookAsyncService, AccountingBookAsyncService>();

            return services;
        }
    }
}
