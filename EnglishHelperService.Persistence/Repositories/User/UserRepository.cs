using EnglishHelperService.Persistence.Common;
using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Persistence.Repositories
{
	/// <summary>
	/// User repository with generic
	/// </summary>
	public class UserRepository : GenericRepository<DataContext, User>, IUserRepository
	{
		public UserRepository(DataContext dbContext) : base(dbContext) { }
	}
}
