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

		public async Task<User> Read(long id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<IEnumerable<User>> List()
		{
			return await _context.Users.ToListAsync();
		}

		public void Create(User user)
		{
			_context.Users.Add(user);
		}

		public void Update(User user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}

		public void Delete(User user)
		{
			_context.Users.Remove(user);
		}
	}
}
