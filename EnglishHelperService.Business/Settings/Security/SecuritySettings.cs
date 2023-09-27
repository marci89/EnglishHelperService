using Microsoft.Extensions.Configuration;

namespace EnglishHelperService.Business.Settings
{
    /// <summary>
    /// SecuritySettings class. Get SecuritySettings section values from appsettings.json
    /// </summary>
    public class SecuritySettings : ISecuritySettings
    {
        public SecuritySettings(IConfiguration configuration)
        {
            TokenKey = configuration.GetSection("SecuritySettings").GetValue<string>("TokenKey");
            TokenExpirationDays = configuration.GetSection("SecuritySettings").GetValue<int>("TokenExpirationDays");
        }

        /// <summary>
        /// token key for jwt token
        /// </summary>
        public string TokenKey { get; private set; }

        /// <summary>
        /// Jwt token expiration days
        /// </summary>
        public int TokenExpirationDays { get; private set; }
    }
}
