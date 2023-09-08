using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;
using LinqKit;
using System.Linq.Expressions;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Managing Users
	/// </summary>
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

		/// <summary>
		/// Create an auth token and login the user
		/// </summary>
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

		/// <summary>
		/// Read user by id
		/// </summary>
		public async Task<User> ReadUserById(long id)
		{
			var user = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
			return _userFactory.Create(user);
		}

		/// <summary>
		/// List users by filtering and return a pagedlist for paginator
		/// </summary>
		public async Task<ListUserResponse> ListUser(ListUserWithFilterRequest request)
		{
			try
			{
				long totalCount;
				var filterExpression = ListUserFiltering(request);
				var orderExpression = ListUserOrdering(request);

				var query = _unitOfWork.UserRepository.PagedQuery(request.PageNumber, request.PageSize, filterExpression, orderExpression, out totalCount);

				var users = query.Select(u => _userFactory.Create(u)).ToList();
				var result = new PagedList<User>(users, totalCount, request.PageNumber, request.PageSize);

				return await Task.FromResult(new ListUserResponse
				{
					StatusCode = StatusCode.Ok,
					Result = result
				});
			}
			catch (Exception ex)
			{
				return await Task.FromResult(new ListUserResponse
				{
					StatusCode = StatusCode.InternalServerError,
					ErrorMessage = ErrorMessage.ServerError

				});
			}
		}

		/// <summary>
		/// Create user
		/// </summary>
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

		/// <summary>
		/// Update User
		/// </summary>
		public async Task Update(UpdateUserRequest request)
		{

		}

		/// <summary>
		/// Delete user by id
		/// </summary>
		public async Task Delete(long id)
		{
			var user = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
			await _unitOfWork.UserRepository.DeleteAsync(user);
			await _unitOfWork.SaveAsync();
		}


		#region private methods


		/// <summary>
		/// Filtering the user table elements by filter
		/// </summary>
		private Expression<Func<Persistence.Entities.User, bool>> ListUserFiltering(ListUserWithFilterRequest request)
		{
			Expression<Func<Persistence.Entities.User, bool>> filterExpression = u => true;

			if (!string.IsNullOrEmpty(request.Username))
			{
				filterExpression = filterExpression.And(u => u.Username.Contains(request.Username));
			}

			if (!string.IsNullOrEmpty(request.Email))
			{
				filterExpression = filterExpression.And(u => u.Email.Contains(request.Email));
			}

			return filterExpression;
		}

		/// <summary>
		/// Ordering the user table elements by request
		/// </summary>
		private Func<IQueryable<Persistence.Entities.User>, IOrderedQueryable<Persistence.Entities.User>> ListUserOrdering(ListUserWithFilterRequest request)
		{
			Func<IQueryable<Persistence.Entities.User>, IOrderedQueryable<Persistence.Entities.User>> orderBy = null;

			if (!string.IsNullOrEmpty(request.FieldName))
			{
				switch (request.FieldName.ToLower())
				{
					case "username":
						orderBy = query => request.IsDescending ?
							query.OrderByDescending(u => u.Username) :
							query.OrderBy(u => u.Username);
						break;
					case "email":
						orderBy = query => request.IsDescending ?
							query.OrderByDescending(u => u.Email) :
							query.OrderBy(u => u.Email);
						break;
					case "role":
						orderBy = query => request.IsDescending ?
							query.OrderByDescending(u => u.Role) :
							query.OrderBy(u => u.Role);
						break;
					case "created":
						orderBy = query => request.IsDescending ?
							query.OrderByDescending(u => u.Created) :
							query.OrderBy(u => u.Created);
						break;
					case "lastactive":
						orderBy = query => request.IsDescending ?
							query.OrderByDescending(u => u.LastActive) :
							query.OrderBy(u => u.LastActive);
						break;
					default:
						orderBy = query => request.IsDescending ?
								query.OrderByDescending(u => u.Username) :
								query.OrderBy(u => u.Username);
						break;
				}
			}

			return orderBy;
		}

		#endregion
	}
}
