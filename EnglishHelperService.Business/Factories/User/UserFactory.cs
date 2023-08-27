
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

		public Models.User Create(User user)
		{
			if (user is null)
				return null;

			return new Models.User
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email,
				Created = user.Created,
			};
		}

		public User Create(Models.CreateUserRequest request)
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

		public Models.LoginUserResponse Create(Models.LoginUserRequest request, User user)
		{
			if (request is null || user is null)
				return null;

			return new Models.LoginUserResponse
			{
				Username = request.Username,
				Token = _tokenService.CreateToken(user)
			};
		}
	}
}
