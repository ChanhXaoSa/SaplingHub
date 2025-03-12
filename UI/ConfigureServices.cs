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
using SH_DataAccessObjects.DAO.Interfaces;
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

            services.AddScoped<ISaplingDAO, SaplingDAO>();
            services.AddScoped<ICartDAO, CartDAO>();
            services.AddScoped<ICategoryDAO, CategoryDAO>();
            services.AddScoped<IOrderDAO, OrderDAO>();
            services.AddScoped<IOrderDetailDAO, OrderDetailDAO>();
            services.AddScoped<IPaymentDAO, PaymentDAO>();
            services.AddScoped<IUserAccountDAO, UserAccountDAO>();
            services.AddScoped<IAdminAccountDAO, AdminAccountDAO>();
            services.AddScoped<IAccountDAO, AccountDAO>();

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

            services.AddAuthentication(opts => opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Bearer", opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
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

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Sapling Hub API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer YOUR_TOKEN')",
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
