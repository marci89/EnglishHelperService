
using EnglishHelperService.Business.Settings;
using EnglishHelperService.ServiceContracts;
using Microsoft.Extensions.Logging;

namespace EnglishHelperService.Business
{
    public class RegisterEmailSender : EmailSenderBase, IRegisterEmailSender
    {
        private readonly ILogger<EmailSenderBase> _logger;

        public RegisterEmailSender(IEmailSettings settings, ILogger<EmailSenderBase> logger) : base(settings, logger)
        {
            _logger = logger;
        }

        public async Task ExecuteAsync(RegisterEmailSenderRequest request)
        {
            try
            {
                //Get the email template
                var bodyTemplate = CreateBody(new CreateEmailBodyRequest
                {
                    EmailTemplateName = "RegisterEmailTemplate",
                    Language = request.Language,
                });

                //Replace variables
                bodyTemplate = bodyTemplate.Replace("{Username}", request.Username);

                request.Body = bodyTemplate;
                request.Subject = CreateSubject(request.Language, request.Username);

                await SendEmailAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Sending email error: {ex.Message}");
            }
        }

        private string CreateSubject(string language, string username)
        {
            if (language == "hu")
                return $"Sikeres regisztráció {username} számára";
            else return $"Successful registration for {username}";
        }
    }
}