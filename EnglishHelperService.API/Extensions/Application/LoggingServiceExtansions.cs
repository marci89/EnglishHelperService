using EnglishHelperService.Business.Settings;
using Serilog;
using Serilog.Events;

namespace EnglishHelperService.API.Extensions
{
	public static class LoggingServiceExtansions
	{
		/// <summary>
		/// Handle logging registration
		/// </summary>
		public static IServiceCollection AddLoggingService(this IServiceCollection services, ILogSettings logSettings)
		{
			//Get file path
			var logFilePath = logSettings.LogFilesPath;

			// Configure Serilog
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
				.CreateLogger();

			services.AddLogging(builder => builder.AddSerilog());

			return services;
		}
	}
}