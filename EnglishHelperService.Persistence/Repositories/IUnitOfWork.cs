using EnglishHelperService.Persistence.Common;

namespace EnglishHelperService.Persistence.Repositories
{
	/// <summary>
	/// Interface of classes that combine database operations(repositories).
	/// </summary>
	public interface IUnitOfWork : IUnitOfWorkBase
	{
		IUserRepository UserRepository { get; }
	}
}
