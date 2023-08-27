using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.Persistence.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _context;

		public UserRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<User> ReadByIdAsync(long id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<User> ReadByNameAsync(string name)
		{
			return await _context.Users.SingleOrDefaultAsync(x =>
			x.Username == name);
		}

		public async Task<IEnumerable<User>> ListAsync()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task CreateAsync(User user)
		{
			await _context.Users.AddAsync(user);
		}

		public Task UpdateAsync(User user)
		{
			_context.Entry(user).State = EntityState.Modified;
			return Task.CompletedTask;
		}

		public async Task DeleteAsync(User user)
		{
			_context.Users.Remove(user);
			await Task.CompletedTask;
		}
	}
}
