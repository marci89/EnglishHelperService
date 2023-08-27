using EnglishHelperService.Business.Models;
using EnglishHelperService.Persistence.UnitOfWork;

namespace EnglishHelperService.Business
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserFactory _userFactory;

		public UserService(IUnitOfWork unitOfWork, UserFactory userFactory)
		{
			_unitOfWork = unitOfWork;
			_userFactory = userFactory;
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

		public async Task CreateAsync(CreateUserRequest request)
		{
			var entityUser = _userFactory.Create(request);
			await _unitOfWork.UserRepository.CreateAsync(entityUser);
			await _unitOfWork.SaveAsync();
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
			var user = await _unitOfWork.UserRepository.ReadByNameAsync(request.Username);
			return _userFactory.Create(request, user);
		}
	}
}
