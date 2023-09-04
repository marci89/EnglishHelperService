
using EnglishHelperService.Persistence.Common;
using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserFactory _userFactory;
		private readonly CreateUserValidator _createUserValidator;
		private readonly LoginUserValidator _loginUserValidator;

		public UserService(
			IUnitOfWork unitOfWork,
			UserFactory userFactory,
			CreateUserValidator createUserValidator,
			LoginUserValidator loginUserValidator
			)
		{
			_unitOfWork = unitOfWork;
			_userFactory = userFactory;
			_createUserValidator = createUserValidator;
			_loginUserValidator = loginUserValidator;
		}

		public async Task<LoginUserResponse> Login(LoginUserRequest request)
		{
			try
			{
				var validationResult = _loginUserValidator.IsValid(request);
				if (!validationResult.HasError)
				{
					var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Username == request.Identifier
					|| u.Email == request.Identifier);

					var user = _userFactory.Create(request, entityUser);
					if (user != null)
					{
						return new LoginUserResponse
						{
							StatusCode = StatusCode.Ok,
							Result = user
						};
					}

					return new LoginUserResponse
					{
						StatusCode = StatusCode.Unauthorized,
						ErrorMessage = ErrorMessage.InvalidPasswordOrUsernameOrEmail
					};
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				return new LoginUserResponse
				{
					StatusCode = StatusCode.InternalServerError,
					ErrorMessage = ErrorMessage.ServerError
				};
			}
		}

		public async Task<User> ReadUserById(long id)
		{
			var user = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
			return _userFactory.Create(user);
		}

		public async Task<ListUserResponse> ListUser(PaginationRequest request)
		{
			try
			{
				long totalCount;

				var query = _unitOfWork.UserRepository.PagedQuery(
					request.PageNumber,
					request.PageSize,
					out totalCount
				);

				var users = query.Select(u => _userFactory.Create(u)).ToList();
				var result = new PagedList<User>(users, totalCount, request.PageNumber, request.PageSize);

				return new ListUserResponse
				{
					StatusCode = StatusCode.Ok,
					Result = result
				};
			}
			catch (Exception ex)
			{
				return new ListUserResponse
				{
					StatusCode = StatusCode.InternalServerError,
					ErrorMessage = ErrorMessage.ServerError
				};
			}
		}

		public async Task<CreateUserResponse> Create(CreateUserRequest request)
		{
			try
			{
				var validationResult = _createUserValidator.IsValid(request);
				if (!validationResult.HasError)
				{
					var entityUser = _userFactory.Create(request);
					await _unitOfWork.UserRepository.CreateAsync(entityUser);
					await _unitOfWork.SaveAsync();

					return new CreateUserResponse
					{
						StatusCode = StatusCode.Created,
						Result = _userFactory.Create(entityUser)
					};
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				return new CreateUserResponse
				{
					StatusCode = StatusCode.InternalServerError,
					ErrorMessage = ErrorMessage.ServerError
				};
			}

		}

		public async Task Update(UpdateUserRequest request)
		{

		}

		public async Task Delete(long id)
		{
			var user = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
			await _unitOfWork.UserRepository.DeleteAsync(user);
			await _unitOfWork.SaveAsync();
		}
	}
}
