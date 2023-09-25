using Microsoft.Extensions.Configuration;

namespace EnglishHelperService.Business.Settings
{

    /// <summary>
    /// EmailSettings class. Get EmailSettings section values from appsettings.json
    /// </summary>
    public class EmailSettings : IEmailSettings
    {
        public EmailSettings(IConfiguration configuration)
        {
            SmtpServer = configuration.GetSection("EmailSettings").GetValue<string>("SmtpServer");
            SmtpPort = configuration.GetSection("EmailSettings").GetValue<int>("SmtpPort");
            SmtpPassword = configuration.GetSection("EmailSettings").GetValue<string>("SmtpPassword");
            SenderEmail = configuration.GetSection("EmailSettings").GetValue<string>("SenderEmail");
            EnableSsl = configuration.GetSection("EmailSettings").GetValue<bool>("EnableSsl");
        }

        /// <summary>
        /// Smtp server
        /// </summary>
        public string SmtpServer { get; private set; }

        /// <summary>
        /// Smtp port
        /// </summary>
        public int SmtpPort { get; private set; }

        /// <summary>
        /// Smtp password
        /// </summary>
        public string SmtpPassword { get; private set; }

        /// <summary>
        /// Sender email
        /// </summary>
        public string SenderEmail { get; private set; }

        /// <summary>
        /// Enable SSL if required by your SMTP server
        /// </summary>
        public bool EnableSsl { get; private set; }
    }
}