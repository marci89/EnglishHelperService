using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	public interface IUserService
	{
		Task<User> ReadUserById(long id);
		Task<IEnumerable<User>> ListUser();
		Task<CreateUserResponse> Create(CreateUserRequest user);
		Task Update(UpdateUserRequest user);
		Task Delete(long id);
		Task<LoginUserResponse> Login(LoginUserRequest request);
	}
}
