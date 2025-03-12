using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Identity;
using SH_BusinessObjects.Identity.Interface;
using SH_BusinessObjects.Services;
using SH_DataAccessObjects.Common.Behaviours;
using SH_DataAccessObjects.Context;
using SH_DataAccessObjects.Context.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessObjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<SaplingHubContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(SaplingHubContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<SaplingHubContext>());

            services.AddScoped<SaplingHubContextInitialiser>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<SaplingHubContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthorizationBuilder()
                .AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly()); 
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            });

            return services;
        }
    }
}
