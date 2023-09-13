using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;
using LinqKit;
using Microsoft.Extensions.Logging;
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
		private readonly UserValidator _validator;
		private readonly ILogger<UserService> _logger;

		public UserService(
			IUnitOfWork unitOfWork,
			UserFactory userFactory,
			UserValidator validator,
			ILogger<UserService> logger
			)
		{
			_unitOfWork = unitOfWork;
			_userFactory = userFactory;
			_validator = validator;
			_logger = logger;
		}

		/// <summary>
		/// Create an auth token and login the user
		/// </summary>
		public async Task<LoginUserResponse> Login(LoginUserRequest request)
		{
			try
			{
				var validationResult = _validator.IsValidLoginRequest(request);
				if (!validationResult.HasError)
				{
					var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Username == request.Identifier
					|| u.Email == request.Identifier);

					var user = _userFactory.Create(request, entityUser);
					if (user is null)
					{
						_logger.LogError("Login failed for user with Identifier: {Identifier}", request.Identifier);

						return _validator.CreateErrorResponse<LoginUserResponse>(
						ErrorMessage.InvalidPasswordOrUsernameOrEmail,
						StatusCode.Unauthorized
						);
					}

					return new LoginUserResponse
					{
						StatusCode = StatusCode.Ok,
						Result = user
					};
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				_logger.LogError("An error occurred during the login process: {ErrorMessage}", ex.Message);
				return await _validator.CreateServerErrorResponse<LoginUserResponse>();
			}
		}

		/// <summary>
		/// Read user by id
		/// </summary>
		public async Task<ReadUserByIdResponse> ReadUserById(long id)
		{
			try
			{
				var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
				var user = _userFactory.Create(entityUser);
				if (user is null)
				{
					_logger.LogWarning("User not found for ID: {UserId}", id);
					return await _validator.CreateNotFoundResponse<ReadUserByIdResponse>();
				}

				return new ReadUserByIdResponse
				{
					StatusCode = StatusCode.Ok,
					Result = user
				};
			}
			catch (Exception ex)
			{
				_logger.LogError("An error occurred while reading user by ID {UserId}: {ErrorMessage}", id, ex.Message);
				return await _validator.CreateServerErrorResponse<ReadUserByIdResponse>();
			}
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
				_logger.LogError("An error occurred while listing users: {ErrorMessage}", ex.Message);
				return await _validator.CreateServerErrorResponse<ListUserResponse>();
			}
		}

		/// <summary>
		/// Create user
		/// </summary>
		public async Task<CreateUserResponse> Create(CreateUserRequest request)
		{
			try
			{
				var validationResult = _validator.IsValidCreateUserRequest(request);
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
				_logger.LogError("An error occurred while create user: {ErrorMessage}", ex.Message);
				return await _validator.CreateCreationErrorResponse<CreateUserResponse>();
			}

		}

		/// <summary>
		/// Update User
		/// </summary>
		public async Task<ResponseBase> Update(UpdateUserRequest request)
		{
			try
			{
				var validationResult = _validator.IsValidUpdateUserRequest(request);
				if (!validationResult.HasError)
				{
					var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == request.Id);
					if (entityUser is null)
					{
						_logger.LogWarning("User not found for update with ID: {UserId}", request.Id);
						return await _validator.CreateNotFoundResponse<ResponseBase>();
					}

					entityUser.Username = request.Username;

					await _unitOfWork.UserRepository.UpdateAsync(entityUser);
					await _unitOfWork.SaveAsync();

					return new ResponseBase();
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				_logger.LogError("An error occurred while updating user with ID {UserId}: {ErrorMessage}", request.Id, ex.Message);
				return await _validator.CreateUpdateErrorResponse<ResponseBase>();
			}

		}

		/// <summary>
		/// Change email
		/// </summary>
		public async Task<ResponseBase> ChangeEmail(ChangeEmailRequest request)
		{
			try
			{
				var validationResult = _validator.IsValidChangeEmailRequest(request);
				if (!validationResult.HasError)
				{
					var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == request.Id);
					if (entityUser is null)
					{
						_logger.LogWarning("User not found for email change with ID: {UserId}", request.Id);
						return await _validator.CreateNotFoundResponse<ResponseBase>();
					}

					entityUser.Email = request.Email;

					await _unitOfWork.UserRepository.UpdateAsync(entityUser);
					await _unitOfWork.SaveAsync();

					return new ResponseBase();
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				_logger.LogError("An error occurred while changing email for user with ID {UserId}: {ErrorMessage}", request.Id, ex.Message);
				return await _validator.CreateUpdateErrorResponse<ResponseBase>();
			}

		}

		/// <summary>
		/// Change email
		/// </summary>
		public async Task<ResponseBase> ChangePassword(ChangePasswordRequest request)
		{
			try
			{
				var validationResult = _validator.IsValidChangePasswordRequest(request);
				if (!validationResult.HasError)
				{
					var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == request.Id);
					if (entityUser is null)
					{
						_logger.LogWarning("User not found for password change with ID: {UserId}", request.Id);
						return await _validator.CreateNotFoundResponse<ResponseBase>();
					}

					entityUser.Password = _userFactory.CreatePasswordHash(request.NewPassword);

					await _unitOfWork.UserRepository.UpdateAsync(entityUser);
					await _unitOfWork.SaveAsync();

					return new ResponseBase();
				}

				return validationResult;
			}
			catch (Exception ex)
			{
				_logger.LogError("An error occurred while changing password for user with ID {UserId}: {ErrorMessage}", request.Id, ex.Message);
				return await _validator.CreateUpdateErrorResponse<ResponseBase>();
			}

		}

		/// <summary>
		/// Delete user by id
		/// </summary>
		public async Task<ResponseBase> Delete(long id)
		{
			try
			{
				var entityUser = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
				if (entityUser is null)
				{
					_logger.LogWarning("User not found for delete with ID: {UserId}", id);
					return await _validator.CreateNotFoundResponse<ResponseBase>();
				}

				await _unitOfWork.UserRepository.DeleteAsync(entityUser);
				await _unitOfWork.SaveAsync();

				return new ResponseBase();
			}
			catch (Exception ex)
			{
				_logger.LogError("An error occurred while deleting user with ID {UserId}: {ErrorMessage}", id, ex.Message);
				return await _validator.CreateDeleteErrorResponse<ResponseBase>();
			}
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
