using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Services;
using SH_DataAccessObjects.Context;
using SH_DataAccessObjects.Context.Interceptor;
using SH_DataAccessObjects.DAO;
using SH_DataAccessObjects.DAO.Interfaces;
using SH_Repositories.Repos;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services;
using SH_Services.Services.Interfaces;

namespace UI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IApplicationDbContext, SaplingHubContext>();

            services.AddScoped<ISaplingDAO, SaplingDAO>();
            services.AddScoped<ICartDAO, CartDAO>();
            services.AddScoped<ICategoryDAO, CategoryDAO>();
            services.AddScoped<IOrderDAO, OrderDAO>();
            services.AddScoped<IOrderDetailDAO, OrderDetailDAO>();
            services.AddScoped<IPaymentDAO, PaymentDAO>();

            services.AddScoped<ISaplingRepository, SaplingRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<ISaplingService, SaplingService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Art Work Sharing API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });
            return services;
        }
    }
}
