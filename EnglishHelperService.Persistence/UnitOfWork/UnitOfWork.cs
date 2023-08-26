using EnglishHelperService.Persistence.Repositories;

namespace EnglishHelperService.Persistence.UnitOfWork
{
	public class UnitOfWork : BaseUnitOfWork<DataContext>
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

