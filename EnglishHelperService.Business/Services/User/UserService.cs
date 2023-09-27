
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
        private readonly UserFactory _factory;
        private readonly UserValidator _validator;

        public UserService(
            IUnitOfWork unitOfWork,
            UserFactory factory,
            UserValidator validator
            )
        {
            _unitOfWork = unitOfWork;
            _factory = factory;
            _validator = validator;
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
                    var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Username == request.Identifier
                    || u.Email == request.Identifier);

                    var result = _factory.Create(request, entity);
                    if (result is null)
                    {
                        return _validator.CreateErrorResponse<LoginUserResponse>(
                        ErrorMessage.InvalidPasswordOrUsernameOrEmail,
                        StatusCode.Unauthorized
                        );
                    }

                    return new LoginUserResponse
                    {
                        StatusCode = StatusCode.Ok,
                        Result = result
                    };
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<LoginUserResponse>(ex.Message);
            }
        }

        /// <summary>
        /// Read user by id
        /// </summary>
        public async Task<ReadUserByIdResponse> ReadById(long id)
        {
            try
            {
                var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
                var result = _factory.Create(entity);
                if (result is null)
                {
                    return await _validator.CreateNotFoundResponse<ReadUserByIdResponse>();
                }

                return new ReadUserByIdResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ReadUserByIdResponse>(ex.Message);
            }
        }

        /// <summary>
        /// List users by filtering and return a pagedlist for paginator
        /// </summary>
        public async Task<ListUserResponse> List(ListUserWithFilterRequest request)
        {
            try
            {
                long totalCount;
                var filterExpression = ListUserFiltering(request);
                var orderExpression = ListUserOrdering(request);

                var query = _unitOfWork.UserRepository.PagedQuery(request.PageNumber, request.PageSize, filterExpression, orderExpression, out totalCount);

                var users = query.Select(u => _factory.Create(u)).ToList();
                var result = new PagedList<User>(users, totalCount, request.PageNumber, request.PageSize);

                return await Task.FromResult(new ListUserResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ListUserResponse>(ex.Message);
            }
        }

        /// <summary>
        /// Create user
        /// </summary>
        public async Task<CreateUserResponse> Create(CreateUserRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidCreateRequest(request);
                if (!validationResult.HasError)
                {
                    var entity = _factory.Create(request);
                    await _unitOfWork.UserRepository.CreateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new CreateUserResponse
                    {
                        StatusCode = StatusCode.Created,
                        Result = _factory.Create(entity)
                    };
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateCreationErrorResponse<CreateUserResponse>(ex.Message);
            }

        }

        /// <summary>
        /// Update User
        /// </summary>
        public async Task<ResponseBase> Update(UpdateUserRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidUpdateRequest(request);
                if (!validationResult.HasError)
                {
                    var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == request.Id);
                    if (entity is null)
                    {
                        return await _validator.CreateNotFoundResponse<ResponseBase>();
                    }

                    entity.Username = request.Username;

                    await _unitOfWork.UserRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new ResponseBase();
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateUpdateErrorResponse<ResponseBase>(ex.Message);
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
                    var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == request.Id);
                    if (entity is null)
                    {
                        return await _validator.CreateNotFoundResponse<ResponseBase>();
                    }

                    entity.Email = request.Email;

                    await _unitOfWork.UserRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new ResponseBase();
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateUpdateErrorResponse<ResponseBase>(ex.Message);
            }

        }

        /// <summary>
        /// Change password
        /// </summary>
        public async Task<ResponseBase> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidChangePasswordRequest(request);
                if (!validationResult.HasError)
                {
                    var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == request.Id);
                    if (entity is null)
                    {
                        return await _validator.CreateNotFoundResponse<ResponseBase>();
                    }

                    entity.Password = _factory.CreatePasswordHash(request.NewPassword);

                    await _unitOfWork.UserRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new ResponseBase();
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateUpdateErrorResponse<ResponseBase>(ex.Message);
            }

        }

        /// <summary>
        /// Forgot password
        /// </summary>
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidForgotPasswordRequest(request);
                if (!validationResult.HasError)
                {
                    // Find the user by username or email
                    var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Username == request.Identifier
                  || u.Email == request.Identifier);

                    if (entity is null)
                    {
                        return _validator.CreateErrorResponse<ForgotPasswordResponse>(ErrorMessage.NoUsernameOrEmailExists);
                    }

                    // Generate a reset token (guid)
                    var resetToken = Guid.NewGuid().ToString().Replace("-", "");

                    // Store the reset token and its expiration in your database
                    entity.ResetToken = resetToken;
                    entity.ResetTokenExpiration = DateTime.UtcNow.AddHours(24);
                    await _unitOfWork.UserRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new ForgotPasswordResponse
                    {
                        Result = new ResetPasswordData
                        {
                            Username = entity.Username,
                            Token = resetToken,
                            Email = entity.Email
                        },
                        StatusCode = StatusCode.Ok
                    };
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ForgotPasswordResponse>(ex.Message);
            }
        }

        /// <summary>
        /// Reset password
        /// </summary>
        public async Task<ResponseBase> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidPassword(request.Password);
                if (!validationResult.HasError)
                {
                    var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.ResetToken == request.Token && u.ResetTokenExpiration > DateTime.UtcNow);
                    if (entity is null)
                    {
                        return _validator.CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidResetToken);
                    }

                    entity.Password = _factory.CreatePasswordHash(request.Password);
                    entity.ResetToken = null;
                    entity.ResetTokenExpiration = null;

                    await _unitOfWork.UserRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new ResponseBase();
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateUpdateErrorResponse<ResponseBase>(ex.Message);
            }

        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        public async Task<ResponseBase> Delete(long id)
        {
            try
            {
                var entity = await _unitOfWork.UserRepository.ReadAsync(u => u.Id == id);
                if (entity is null)
                {
                    return await _validator.CreateNotFoundResponse<ResponseBase>();
                }

                await _unitOfWork.UserRepository.DeleteAsync(entity);
                await _unitOfWork.SaveAsync();

                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return await _validator.CreateDeleteErrorResponse<ResponseBase>(ex.Message);
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
