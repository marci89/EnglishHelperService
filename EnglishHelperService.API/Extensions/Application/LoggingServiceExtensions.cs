using EnglishHelperService.Business.Settings;
using Serilog;
using Serilog.Events;

namespace EnglishHelperService.API.Extensions
{
    public static class LoggingServiceExtensions
    {
        /// <summary>
        /// Handle logging registration
        /// </summary>
        public static IServiceCollection AddLoggingService(this IServiceCollection services, ILogSettings settings)
        {
            //Get file path
            var logFilePath = settings.LogFilesPath;

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog());

            return services;
        }
    }
}