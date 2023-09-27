using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
    public interface IResetPasswordEmailSender
    {
        Task<ResponseBase> ExecuteAsync(ResetPasswordEmailSenderRequest request);
    }
}
