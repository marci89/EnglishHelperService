
using EnglishHelperService.Persistence.UnitOfWork;
using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserFactory _userFactory;
		private readonly CreateUserValidator _createUserValidator;

		public UserService(
			IUnitOfWork unitOfWork,
			UserFactory userFactory,
			CreateUserValidator createUserValidator)
		{
			_unitOfWork = unitOfWork;
			_userFactory = userFactory;
			_createUserValidator = createUserValidator;
		}

		public async Task<User> ReadUserByIdAsync(long id)
		{
			var user = await _unitOfWork.UserRepository.ReadByIdAsync(id);
			return _userFactory.Create(user);
		}

		public async Task<IEnumerable<User>> ListUserAsync()
		{
			var users = await _unitOfWork.UserRepository.ListAsync();
			return users.Select(x => _userFactory.Create(x)).ToList();
		}

		public async Task<CreateUserResponse> CreateAsync(CreateUserRequest request)
		{
			try
			{
				var validationResult = _createUserValidator.IsValid(request);
				if (!validationResult.HasError)
				{
					var entityUser = _userFactory.Create(request);
					await _unitOfWork.UserRepository.CreateAsync(entityUser);
					await _unitOfWork.SaveAsync();
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				return new CreateUserResponse
				{
					StatusCode = StatusCode.INTERNAL_SERVER_ERROR,
					ErrorMessage = ErrorMessage.SERVER_ERROR
				};
			}

		}

		public async Task UpdateAsync(UpdateUserRequest request)
		{

		}

		public async Task DeleteAsync(long id)
		{
			var user = await _unitOfWork.UserRepository.ReadByIdAsync(id);
			await _unitOfWork.UserRepository.DeleteAsync(user);
			await _unitOfWork.SaveAsync();
		}

		public async Task<LoginUserResponse> Login(LoginUserRequest request)
		{
			var dbUser = await _unitOfWork.UserRepository.ReadByNameAsync(request.Username);
			var user = _userFactory.Create(request, dbUser);
			return await Task.FromResult(new LoginUserResponse
			{
				Result = user,
			});
		}
	}
}
