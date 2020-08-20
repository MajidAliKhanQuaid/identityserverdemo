using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiTBS_MediatR.Pipes;
using ApiToBeSecured.Data;
using ApiToBeSecured.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace ApiToBeSecured
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // allows you to access http context
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserIdPipe<,>));
            services.AddMediatR(typeof(ApiTBS_MediatR.ApiTBS_MediatR).Assembly);

            services.AddDbContext<ECommDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = Config.IDENTITY_SERVER_URL;
                    config.Audience = Config.WHO_AM_I;
                    config.RequireHttpsMetadata = false;
                    //config.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuerSigningKey = false,
                    //    ValidateIssuer = false,
                    //    ValidateAudience = false
                    //};
                });


            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductTagService, ProductTagService>();
            services.AddTransient<IShipmentService, ShipmentService>();

            services.AddCors(confg =>
                confg.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithHeaders(HeaderNames.ContentType)
                        .WithMethods("PUT", "POST", "GET")
                        )
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();

            app.UseStaticFiles();
            //app.Use(async (context, next) =>
            //{
            //    // Call the next delegate/middleware in the pipeline
            //    await next();
            //});

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
