using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	public interface IUserService
	{
		Task<User> ReadUserById(long id);
		Task<ListUserResponse> ListUser(PaginationRequest request);
		Task<CreateUserResponse> Create(CreateUserRequest user);
		Task Update(UpdateUserRequest user);
		Task Delete(long id);
		Task<LoginUserResponse> Login(LoginUserRequest request);
	}
}
