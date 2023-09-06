using EnglishHelperService.Persistence.Common;

namespace EnglishHelperService.Persistence.Repositories
{
	/// <summary>
	/// class that combine database operations(repositories).
	/// </summary>
	public class UnitOfWork : UnitOfWorkBase<DataContext>, IUnitOfWork
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
