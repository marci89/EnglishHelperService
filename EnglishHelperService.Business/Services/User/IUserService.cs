using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	public interface IUserService
	{
		Task<LoginUserResponse> Login(LoginUserRequest request);
		Task<ReadUserByIdResponse> ReadUserById(long id);
		Task<ListUserResponse> ListUser(ListUserWithFilterRequest request);
		Task<CreateUserResponse> Create(CreateUserRequest request);
		Task<ResponseBase> Update(UpdateUserRequest request);
		Task<ResponseBase> ChangeEmail(ChangeEmailRequest request);
		Task<ResponseBase> ChangePassword(ChangePasswordRequest request);
		Task<ResponseBase> Delete(long id);
	}
}
