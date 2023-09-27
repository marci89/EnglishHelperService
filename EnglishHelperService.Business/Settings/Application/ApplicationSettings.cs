using Microsoft.Extensions.Configuration;

namespace EnglishHelperService.Business.Settings
{
    /// <summary>
    /// ApplicationSettings class. Get ApplicationSettings section values from appsettings.json
    /// </summary>
    public class ApplicationSettings : IApplicationSettings
    {
        public ApplicationSettings(IConfiguration configuration)
        {
            ClientDomain = configuration.GetSection("ApplicationSettings").GetValue<string>("ClientDomain");
            ApplicationName = configuration.GetSection("ApplicationSettings").GetValue<string>("ApplicationName");
        }

        /// <summary>
        /// Client domain url
        /// </summary>
        public string ClientDomain { get; private set; }

        /// <summary>
        /// Application name
        /// </summary>
        public string ApplicationName { get; private set; }
    }
}
