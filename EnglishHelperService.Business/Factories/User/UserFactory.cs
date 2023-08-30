
using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{
	public class UserFactory
	{
		private readonly PasswordSecurityHandler _passwordSecurityHandler;
		private readonly ITokenService _tokenService;

		public UserFactory(PasswordSecurityHandler passwordSecurityHandler, ITokenService tokenService)
		{
			_passwordSecurityHandler = passwordSecurityHandler;
			_tokenService = tokenService;
		}

		public ServiceContracts.User Create(User user)
		{
			if (user is null)
				return null;

			return new ServiceContracts.User
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email,
				Created = user.Created,
			};
		}

		public User Create(ServiceContracts.CreateUserRequest request)
		{
			if (request is null)
				return null;

			var passwordResult = _passwordSecurityHandler.CreatePassword(request.Password);

			return new User
			{
				Username = request.Username,
				Email = request.Email,
				PasswordHash = passwordResult.PasswordHash,
				PasswordSalt = passwordResult.PasswordSalt,
				Created = DateTime.UtcNow
			};
		}

		public ServiceContracts.LoginUser Create(ServiceContracts.LoginUserRequest request, User user)
		{
			if (request is null || user is null)
				return null;

			return new ServiceContracts.LoginUser
			{
				Username = request.Username,
				Token = _tokenService.CreateToken(user)
			};
		}
	}
}
