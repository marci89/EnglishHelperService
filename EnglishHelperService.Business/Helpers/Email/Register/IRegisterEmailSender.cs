using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
    public interface IRegisterEmailSender
    {
        Task ExecuteAsync(RegisterEmailSenderRequest request);
    }
}
