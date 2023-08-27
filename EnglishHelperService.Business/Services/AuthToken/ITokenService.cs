using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}
