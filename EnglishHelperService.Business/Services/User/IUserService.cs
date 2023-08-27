using EnglishHelperService.Business.Models;

namespace EnglishHelperService.Business
{
	public interface IUserService
	{
		Task<User> ReadUserById(long id);
		Task<IEnumerable<User>> ListUser();
		void Create(CreateUserRequest user);
		void Update(UpdateUserRequest user);
		void Delete(long id);
	}
}
