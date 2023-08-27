using EnglishHelperService.Persistence.Repositories;

namespace EnglishHelperService.Persistence.UnitOfWork
{
	public interface IUnitOfWork : IBaseUnitOfWork
	{
		IUserRepository UserRepository { get; }
	}
}
