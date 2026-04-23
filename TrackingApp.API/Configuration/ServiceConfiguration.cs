using Microsoft.EntityFrameworkCore;
using TrackingApp.Core.Abstract.Repositories;
using TrackingApp.Repository;
using TrackingApp.Repository.Abstract.Services;
using TrackingApp.Repository.Repositories;
using TrackingApp.Services.Services;
using AppContext = TrackingApp.Repository.AppContext;
namespace TrackingApp.API.Configuration
{
    public static class ServiceConfiguration
    {

        public static IServiceCollection AddDIMethods(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserRepository, UserRepository>();    
            return services;
        }

        public static IServiceCollection EFCoreConfiguration(this IServiceCollection services,string connectionString)
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


        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services) { 
        
            services.AddAutoMapper(typeof(TrackingApp.Services.Profiles.UserProfile).Assembly);
            return services;
        }

    }
}
