
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
				Role = Create(user.Role),
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
				Role = RoleType.Member,
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
				Role = Create(user.Role),
				Token = _tokenService.CreateToken(user)
			};
		}

		public ServiceContracts.RoleType Create(RoleType roleType)
		{
			switch (roleType)
			{
				case RoleType.Admin:
					return ServiceContracts.RoleType.Admin;
				case RoleType.Member:
					return ServiceContracts.RoleType.Member;
				default:
					throw new ArgumentException("Invalid role type.");
			}
		}

	}
}
