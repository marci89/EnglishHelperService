using EnglishHelperService.API.Helpers;
using EnglishHelperService.Business;
using EnglishHelperService.Business.Settings;
using EnglishHelperService.Persistence.Repositories;

namespace EnglishHelperService.API.Extensions
{
    public static class DependencyInjectionServiceExtensions
    {
        /// <summary>
        /// Injecting all of classes
        /// </summary>
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterFactories(services);
            RegisterValidators(services);
            RegisterHelpers(services);

            return services;
        }

        /// <summary>
        /// Register repositories
        /// </summary>
        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Register services
        /// </summary>
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        /// <summary>
        /// Register factories
        /// </summary>
        private static void RegisterFactories(this IServiceCollection services)
        {
            services.AddScoped<UserFactory>();
        }

        /// <summary>
        /// Register validators
        /// </summary>
        private static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<UserValidator>();
        }

        /// <summary>
        /// Register handlers
        /// </summary>
        private static void RegisterHelpers(this IServiceCollection services)
        {
            services.AddScoped<PasswordSecurityHandler>();
            services.AddScoped<LogUserActivity>();
            services.AddScoped<ErrorLogger>();

            //appsettings
            services.AddSingleton<ILogSettings, LogSettings>();
            services.AddSingleton<IDatabaseSettings, DatabaseSettings>();
        }
    }
}
