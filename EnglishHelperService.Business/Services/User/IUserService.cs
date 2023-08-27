using EnglishHelperService.Business.Models;

namespace EnglishHelperService.Business
{
	public interface IUserService
	{
		Task<User> ReadUserByIdAsync(long id);
		Task<IEnumerable<User>> ListUserAsync();
		Task CreateAsync(CreateUserRequest user);
		Task UpdateAsync(UpdateUserRequest user);
		Task DeleteAsync(long id);
		Task<LoginUserResponse> Login(LoginUserRequest request);
	}
}
