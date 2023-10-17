using EnglishHelperService.Business.Settings;
using EnglishHelperService.ServiceContracts;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace EnglishHelperService.Business
{
    /// <summary>
    /// Basic email sender class for helping another email sender classes
    /// </summary>
    public class EmailSenderBase : IEmailSenderBase
    {
        private readonly IEmailSettings _settings;
        private readonly ILogger<EmailSenderBase> _logger;

        //base path for email templates
        const string BaseEmailPath = "Resources/Email/";

        public EmailSenderBase(IEmailSettings settings, ILogger<EmailSenderBase> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        /// <summary>
        /// Send an email with parameters and appsettings values
        /// </summary>
        public async Task SendEmailAsync(SendEmailRequestBase request)
        {
            // Create a new SmtpClient
            using (SmtpClient smtpClient = new SmtpClient(_settings.SmtpServer))
            {
                smtpClient.Port = _settings.SmtpPort;
                smtpClient.Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SmtpPassword);
                smtpClient.EnableSsl = _settings.EnableSsl; // Enable SSL if required by your SMTP server

                // Create a new MailMessage
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_settings.SenderEmail);
                    mailMessage.To.Add(request.RecipientEmail);
                    mailMessage.Subject = request.Subject;
                    mailMessage.Body = request.Body;
                    // Set IsBodyHtml to true to indicate that the email body is in HTML format
                    mailMessage.IsBodyHtml = true;

                    try
                    {
                        // Send the email
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Email connection error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public string CreateBody(CreateEmailBodyRequest request)
        {
            string bodyTemplate = "";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseEmailPath, $"{request.EmailTemplateName}_{request.Language}.html");

            try
            {
                // Check if the template file exists before reading it
                if (File.Exists(fullPath))
                {
                    bodyTemplate = File.ReadAllText(fullPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reading email template error: {ex.Message}");
            }

            return bodyTemplate;
        }
    }
}
