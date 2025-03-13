using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Services;
using SH_DataAccessObjects.Context;
using SH_DataAccessObjects.Context.Interceptor;
using SH_DataAccessObjects.DAO;
using SH_Repositories.Repos;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services;
using SH_Services.Services.Interfaces;
using System.Text;

namespace UI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtIssuer = configuration.GetValue<string>("Jwt:Issuer");
            var jwtAudience = configuration.GetValue<string>("Jwt:Audience");
            var jwtSecretKey = configuration.GetValue<string>("Jwt:Key");

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IApplicationDbContext, SaplingHubContext>();

            services.AddScoped<SaplingDAO>();
            services.AddScoped<CartDAO>();
            services.AddScoped<CategoryDAO>();
            services.AddScoped<OrderDAO>();
            services.AddScoped<OrderDetailDAO>();
            services.AddScoped<PaymentDAO>();
            services.AddScoped<UserAccountDAO>();
            services.AddScoped<AdminAccountDAO>();
            services.AddScoped<AccountDAO>();

            services.AddScoped<ISaplingRepository, SaplingRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IAdminAccountRepository, AdminAccountRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ISaplingService, SaplingService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddAuthentication(options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = jwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey!)),
                    ValidateLifetime = true,
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sapling Hub API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer YOUR_TOKEN')",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
