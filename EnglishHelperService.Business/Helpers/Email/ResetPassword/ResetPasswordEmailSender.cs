using EnglishHelperService.Business.Settings;
using EnglishHelperService.ServiceContracts;
using Microsoft.Extensions.Logging;

namespace EnglishHelperService.Business
{
    public class ResetPasswordEmailSender : EmailSenderBase, IResetPasswordEmailSender
    {
        private readonly ILogger<EmailSenderBase> _logger;

        public ResetPasswordEmailSender(
            IEmailSettings emailSettings,
            ILogger<EmailSenderBase> logger
            ) : base(emailSettings, logger)
        {
            _logger = logger;
        }

        public async Task<ResponseBase> ExecuteAsync(ResetPasswordEmailSenderRequest request)
        {
            try
            {
                //Get the email template
                var bodyTemplate = CreateBody(new CreateEmailBodyRequest
                {
                    EmailTemplateName = "ResetPasswordEmailTemplate",
                    Language = request.Language,
                });

                //Replace variables
                bodyTemplate = bodyTemplate.Replace("{Username}", request.Username);
                bodyTemplate = bodyTemplate.Replace("{ApplicationName}", request.ApplicationName);
                bodyTemplate = bodyTemplate.Replace("{ResetPasswordLink}", request.ResetPasswordLink);

                request.Body = bodyTemplate;
                request.Subject = CreateSubject(request.Language, request.Username);

                await SendEmailAsync(request);

                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return new ResponseBase
                {
                    ErrorMessage = ErrorMessage.SendEmailFailed,
                    ExceptionErrorMessage = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private string CreateSubject(string language, string username)
        {
            if (language == "hu")
                return $"Jelszó-visszaállítás {username} számára: " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            else return $"Reset password for {username} : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }
    }
}
