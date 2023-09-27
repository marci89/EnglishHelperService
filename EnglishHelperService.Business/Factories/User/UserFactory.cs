
using EnglishHelperService.ServiceContracts;
using Entity = EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{
    /// <summary>
    /// User object mapping
    /// </summary>
    public class UserFactory
    {
        private readonly PasswordSecurityHandler _passwordSecurityHandler;
        private readonly UserValidator _validator;
        private readonly ITokenService _tokenService;

        public UserFactory(PasswordSecurityHandler passwordSecurityHandler, UserValidator validator, ITokenService tokenService)
        {
            _passwordSecurityHandler = passwordSecurityHandler;
            _tokenService = tokenService;
            _validator = validator;
        }

        /// <summary>
        /// Map client user from domain user
        /// </summary>
        public User Create(Entity.User request)
        {
            if (request is null)
                return null;

            return new User
            {
                Id = request.Id,
                Role = request.Role.ToString(),
                Username = request.Username,
                Email = request.Email,
                Created = request.Created,
                LastActive = request.LastActive,
            };
        }

        /// <summary>
        /// Map domain user from client user (registration)
        /// </summary>
        public Entity.User Create(CreateUserRequest request)
        {
            if (request is null)
                return null;

            return new Entity.User
            {
                Role = Entity.RoleType.Member,
                Username = request.Username,
                Email = request.Email,
                Password = _passwordSecurityHandler.HashPassword(request.Password),
                Created = DateTime.UtcNow,
                LastActive = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Map login user object from domain user and login datas
        /// </summary>
        public LoginUser Create(LoginUserRequest request, Entity.User user)
        {
            if (request is null || user is null)
                return null;

            var passwordMatchValidation = _validator.IsValidPasswordMatch(user.Id, request.Password);
            if (passwordMatchValidation.HasError)
                return null;

            return new LoginUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = _tokenService.CreateToken(user)
            };
        }

        public string CreatePasswordHash(string password)
        {
            return _passwordSecurityHandler.HashPassword(password);
        }
    }
}
