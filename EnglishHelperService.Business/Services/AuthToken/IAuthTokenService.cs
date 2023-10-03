using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{
	public interface IAuthTokenService
	{
		string CreateToken(User user);
	}
}
