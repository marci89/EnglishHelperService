using EnglishHelperService.Persistence.Repositories;

namespace EnglishHelperService.Persistence.Common
{
	public interface IUnitOfWork : IBaseUnitOfWork
	{
		IUserRepository UserRepository { get; }
	}
}
