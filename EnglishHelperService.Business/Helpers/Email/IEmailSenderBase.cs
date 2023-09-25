using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
    public interface IEmailSenderBase
    {
        Task SendEmailAsync(SendEmailRequestBase request);
    }
}
