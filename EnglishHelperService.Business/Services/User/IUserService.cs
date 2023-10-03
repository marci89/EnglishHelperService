using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
    public interface IUserService
    {
        Task<LoginUserResponse> Login(LoginUserRequest request);
        Task<ReadUserByIdResponse> ReadById(long id);
        Task<ListUserResponse> List(ListUserWithFilterRequest request);
        Task<CreateUserResponse> Create(CreateUserRequest request);
        Task<ResponseBase> Update(UpdateUserRequest request);
        Task<ResponseBase> ChangeEmail(ChangeEmailRequest request, long userId);
        Task<ResponseBase> ChangePassword(ChangePasswordRequest request, long userId);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request);
        Task<ResponseBase> ResetPassword(ResetPasswordRequest request);
        Task<ResponseBase> Delete(long id);
    }
}
