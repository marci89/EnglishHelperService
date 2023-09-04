using EnglishHelperService.Persistence.Common;
using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Persistence.Repositories
{
	public class UserRepository : GenericRepository<DataContext, User>, IUserRepository
	{
		public UserRepository(DataContext dbContext) : base(dbContext) { }
	}
}
