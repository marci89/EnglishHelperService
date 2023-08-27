using EnglishHelperService.Business.Models;
using EnglishHelperService.Persistence.UnitOfWork;
using System.Linq;

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

		public async Task<User> ReadUserById(long id)
		{
			var user =await _unitOfWork.UserRepository.ReadById(id);
			return  _userFactory.Create(user);		
		}

		public async Task<IEnumerable<User>> ListUser()
		{
			var users = await _unitOfWork.UserRepository.List();
			return users.Select(x => _userFactory.Create(x)).ToList();
		}

		public void Create(CreateUserRequest request)
		{	
			var entityUser = _userFactory.Create(request);
		    _unitOfWork.UserRepository.Create(entityUser);
			_unitOfWork.Save();
		}

		public void Update(UpdateUserRequest request)
		{
		}

		public async void Delete(long id)
		{
			var user =  await _unitOfWork.UserRepository.ReadById(id);
			_unitOfWork.UserRepository.Delete(user);
			_unitOfWork.Save();
		}
	}
}
