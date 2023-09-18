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
        Task<ResponseBase> ChangeEmail(ChangeEmailRequest request);
        Task<ResponseBase> ChangePassword(ChangePasswordRequest request);
        Task<ResponseBase> Delete(long id);
    }
}
