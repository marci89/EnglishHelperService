using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Persistence.Repositories
{
	public interface IUserRepository
	{
		Task<User> ReadByIdAsync(long id);
		Task<IEnumerable<User>> ListAsync();
		Task CreateAsync(User user);
		Task UpdateAsync(User user);
		Task DeleteAsync(User user);
	}
}
