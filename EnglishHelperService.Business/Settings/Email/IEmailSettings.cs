namespace EnglishHelperService.Business.Settings
{

    /// <summary>
    ///  IEmailSettings interface
    /// </summary>
    public interface IEmailSettings
    {
        /// <summary>
        /// Smtp server
        /// </summary>
        string SmtpServer { get; }

        /// <summary>
        /// Smtp port
        /// </summary>
        int SmtpPort { get; }

        /// <summary>
        /// Smtp password
        /// </summary>
        string SmtpPassword { get; }

        /// <summary>
        /// Sender email
        /// </summary>
        string SenderEmail { get; }


        /// <summary>
        /// Enable SSL if required by your SMTP server
        /// </summary>
        bool EnableSsl { get; }

    }
}
