using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nadin.Application.Common.Settings;
using Nadin.Application.Contracts.Infrastructure;
using Nadin.Application.Contracts.Persistence;
using Nadin.Infrastucture.Persistence;
using Nadin.Infrastucture.Repositories;
using Nadin.Infrastucture.Services;
using Newtonsoft.Json;
using SharedKernel.Contracts.Infrastructure;
using SharedKernel.Services;
using System.Net;
using System.Text;

namespace Nadin.Infrastucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BearerTokensConfigurationModel>(configuration.GetSection(BearerTokensConfigurationModel.NAME));

            services.AddDbContext<NadinDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(NadinDbContext).Assembly.FullName))
                , ServiceLifetime.Scoped);

            services.AddScoped<NadinDbContextInitializer>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<ITokenCacheService, TokenCacheService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserContextService, UserContextService>();

            services.AddTransient<IDistributedCacheWrapper, DistributedCacheWrapper>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            ConfigureAuthentication(services, configuration);
            ConfigureSwaggerGen(services);

            return services;
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["BearerTokens:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerTokens:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(configuration.GetValue<int>("BearerTokens:ClockSkew"))
                };
            });
        }
        private static void ConfigureSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nadin-Swagger", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme,Id = "Bearer"}},
                        new string[] {}
                    }
                });
            });
        }

        public static void ChangeModelStateInvalidModel(this ApiBehaviorOptions opt)
        {
            opt.InvalidModelStateResponseFactory =
            context => new BadRequestObjectResult(
                    JsonConvert.SerializeObject(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        error = string.Join(Environment.NewLine,
                            context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()
                            )
                    }));
        }
    }
}
