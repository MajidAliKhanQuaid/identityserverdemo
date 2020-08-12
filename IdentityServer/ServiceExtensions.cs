using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class ServiceExtensions
    {
        public static void ConfigureUserIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            string conStr = configuration.GetConnectionString("Default");
            services.AddDbContext<AppIdentityContext>(config =>
            {
                //config.UseSqlServer(conStr);
                config.UseInMemoryDatabase("IdentityDb");
            });
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();
        }

    }
}
