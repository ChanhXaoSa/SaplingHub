using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Identity.Interface;
using SH_BusinessObjects.Identity;
using SH_BusinessObjects.Services;
using SH_DataAccessObjects.Context;
using SH_DataAccessObjects.Context.Interceptor;
using SH_DataAccessObjects.DAO;
using SH_Repositories.Repos;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services;
using SH_Services.Services.Interfaces;
using System.Reflection;
using System.Text;
using Microsoft.Identity.Client;
using SH_DataAccessObjects.DAO.Interfaces;
using StackExchange.Redis;

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
            services.AddScoped<ICartDAO, CartDAO>();
            services.AddScoped<CategoryDAO>();
            services.AddScoped<OrderDAO>();
            services.AddScoped<IOrderDetailDAO, OrderDetailDAO>();
            services.AddScoped<PaymentDAO>();
            services.AddScoped<UserAccountDAO>();
            services.AddScoped<AdminAccountDAO>();
            services.AddScoped<AccountDAO>();
            services.AddScoped<IAuctionBidDAO, AuctionBidDAO>();
            services.AddScoped<IAuctionPlantDAO, AuctionPlantDAO>();

            services.AddScoped<ISaplingRepository, SaplingRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IAdminAccountRepository, AdminAccountRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAuctionBidRepository, AuctionBidRepository>();
            services.AddScoped<IAuctionPlantRepository, AuctionPlantRepository>();

            services.AddScoped<ISaplingService, SaplingService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuctionBidService, AuctionBidService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var redisConnectionString = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(redisConnectionString);
            });

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
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

            services.AddAuthorizationBuilder()
                .AddPolicy("CanPurge", policy => policy.RequireRole("Admin"));

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

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<SaplingHubContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(SaplingHubContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<SaplingHubContext>());

            services.AddScoped<SaplingHubContextInitialiser>();

            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SaplingHubContext>()
                .AddTokenProvider("SaplingHubApi", typeof(DataProtectorTokenProvider<ApplicationUser>))
                .AddDefaultTokenProviders()
                .AddSignInManager();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
