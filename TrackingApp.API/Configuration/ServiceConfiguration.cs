using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrackingApp.Core.Abstract.Repositories;
using TrackingApp.Core.Abstract.Services;
using TrackingApp.Core.HelperFunctions;
using TrackingApp.Core.Options;
using TrackingApp.Repository;
using TrackingApp.Repository.Abstract.Services;
using TrackingApp.Repository.Repositories;
using TrackingApp.Services.Services;
using AppContext = TrackingApp.Repository.AppContext;
using IAuthorizationService = TrackingApp.Core.Abstract.Services.IAuthorizationService;
namespace TrackingApp.API.Configuration
{
    public static class ServiceConfiguration
    {

        public static IServiceCollection AddDIMethods(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();  
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection EFCoreConfiguration(this IServiceCollection services, string connectionString)
        {


            services.AddDbContext<AppContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );

                });


            });


            return services;
        }

        public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<Core.Options.JwtSettingsOptions>(configuration.GetSection("JwtSettings"));
            return services;
        }


        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(TrackingApp.Services.Profiles.UserProfile).Assembly);
            return services;
        }

        public static IServiceCollection AddJwtTokenConfiguration(this IServiceCollection services, JwtSettingsOptions settings)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;  
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = settings.ValidateIssuer,
                    ValidateAudience = settings.ValidateAudience,
                    ValidateLifetime = settings.ValidateLifetime,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = settings.Audience,
                    ClockSkew = TimeSpan.Zero,  
                    LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(HelperGeneral.Decrypt(settings.SecretKey))),
                    

                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = async context =>
                    {

                        //Cookie olarak geliyor ise token'ı al web için
                        var accessToken = context.Request.Cookies[HelperGeneral.GetCookieName()];
                        if (!string.IsNullOrEmpty(accessToken))
                            context.Token = accessToken;
                        
                    }
                };
            });

            return services;
        }

    }
}
