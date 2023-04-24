using GroupBy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GroupBy.Application.Identity
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddApplicationStores(this IdentityBuilder builder)
        {
            builder.Services.AddTransient<IRoleStore<IdentityRole<Guid>>, ApplicationRoleStore>();
            builder.Services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            return builder;
        }
    }
}
