using EnglishHelperService.Persistence.Repositories;

namespace EnglishHelperService.Persistence.Common
{
	public class UnitOfWork : BaseUnitOfWork<DataContext>, IUnitOfWork
	{
		public UnitOfWork(
			DataContext context,
			IUserRepository userRepository
			) : base(context)
		{
			UserRepository = userRepository;
		}

		public virtual IUserRepository UserRepository { get; }

	}
}

