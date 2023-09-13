using Microsoft.Extensions.Configuration;

namespace EnglishHelperService.Business.Settings
{

	/// <summary>
	/// LogSettings class. Get LogSettings section values from appsettings.json
	/// </summary>
	public class LogSettings : ILogSettings
	{
		public LogSettings(IConfiguration configuration)
		{
			LogFilesPath = configuration.GetSection("LogSettings:LogFilesPath").Value;
		}

		/// <summary>
		/// Log files path
		/// </summary>
		public string LogFilesPath { get; private set; }

	}
}