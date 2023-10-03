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
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<ILearnStatisticsRepository, LearnStatisticsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Register services
        /// </summary>
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<ILearnStatisticsService, LearnStatisticsService>();
            services.AddScoped<IAuthTokenService, AuthTokenService>();
        }

        /// <summary>
        /// Register factories
        /// </summary>
        private static void RegisterFactories(this IServiceCollection services)
        {
            services.AddScoped<UserFactory>();
            services.AddScoped<WordFactory>();
            services.AddScoped<LearnStatisticsFactory>();
        }

        /// <summary>
        /// Register validators
        /// </summary>
        private static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<UserValidator>();
            services.AddScoped<WordValidator>();
            services.AddScoped<LearnStatisticsValidator>();
        }

        /// <summary>
        /// Register handlers
        /// </summary>
        private static void RegisterHelpers(this IServiceCollection services)
        {
            services.AddScoped<PasswordSecurityHandler>();
            services.AddScoped<LogUserActivity>();
            services.AddScoped<ErrorLogger>();

            //emails
            services.AddScoped<IEmailSenderBase, EmailSenderBase>();
            services.AddScoped<IRegisterEmailSender, RegisterEmailSender>();
            services.AddScoped<IResetPasswordEmailSender, ResetPasswordEmailSender>();

            //appsettings
            services.AddSingleton<IApplicationSettings, ApplicationSettings>();
            services.AddSingleton<ISecuritySettings, SecuritySettings>();
            services.AddSingleton<ILogSettings, LogSettings>();
            services.AddSingleton<IDatabaseSettings, DatabaseSettings>();
            services.AddSingleton<IEmailSettings, EmailSettings>();
        }
    }
}
