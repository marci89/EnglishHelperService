using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;
using System.Text.RegularExpressions;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Validating user object
	/// </summary>
	public class UserValidator : BaseValidator
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly PasswordSecurityHandler _passwordSecurityHandler;

		public UserValidator(IUnitOfWork unitOfWork, PasswordSecurityHandler passwordSecurityHandler)
		{
			_unitOfWork = unitOfWork;
			_passwordSecurityHandler = passwordSecurityHandler;
		}

		/// <summary>
		/// Execute user login request validating
		/// </summary>
		public LoginUserResponse IsValidLoginRequest(LoginUserRequest request)
		{

			if (request is null)
				return CreateErrorResponse<LoginUserResponse>(ErrorMessage.InvalidRequest);

			if (String.IsNullOrWhiteSpace(request.Identifier))
				return CreateErrorResponse<LoginUserResponse>(ErrorMessage.UsernameOrEmailRequired);

			if (String.IsNullOrWhiteSpace(request.Password))
				return CreateErrorResponse<LoginUserResponse>(ErrorMessage.PasswordRequired);

			return new LoginUserResponse
			{
				StatusCode = StatusCode.Ok,
			};
		}

		/// <summary>
		/// Execute user create request validating
		/// </summary>
		public CreateUserResponse IsValidCreateUserRequest(CreateUserRequest request)
		{

			if (request is null)
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.InvalidRequest);

			if (String.IsNullOrWhiteSpace(request.Username))
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.UsernameRequired);

			if (_unitOfWork.UserRepository.Count(u => u.Username == request.Username) > 0)
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.UsernameExists);

			if (request.Username.Length > 50)
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.UsernameMaxLength);

			if (String.IsNullOrWhiteSpace(request.Password))
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.PasswordRequired);

			if (request.Password.Length < 4)
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.InvalidPasswordFormat);

			if (String.IsNullOrWhiteSpace(request.Email))
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.EmailRequired);

			if (!IsValidEmail(request.Email))
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.InvalidEmailFormat);


			if (_unitOfWork.UserRepository.Count(u => u.Email == request.Email) > 0)
				return CreateErrorResponse<CreateUserResponse>(ErrorMessage.EmailExists);

			return new CreateUserResponse
			{
				StatusCode = StatusCode.Created,
			};
		}

		/// <summary>
		/// Execute user update request validating
		/// </summary>
		public UpdateUserResponse IsValidUpdateUserRequest(UpdateUserRequest request)
		{

			if (String.IsNullOrWhiteSpace(request.Username))
				return CreateErrorResponse<UpdateUserResponse>(ErrorMessage.UsernameRequired);

			if (_unitOfWork.UserRepository.Count(u => u.Username == request.Username) > 0)
				return CreateErrorResponse<UpdateUserResponse>(ErrorMessage.UsernameExists);

			if (request.Username.Length > 50)
				return CreateErrorResponse<UpdateUserResponse>(ErrorMessage.UsernameMaxLength);

			return new UpdateUserResponse
			{
				StatusCode = StatusCode.Ok,
			};
		}

		/// <summary>
		/// Execute change email request validating
		/// </summary>
		public ResponseBase IsValidChangeEmailRequest(ChangeEmailRequest request)
		{

			if (String.IsNullOrWhiteSpace(request.Password))
				return CreateErrorResponse<ResponseBase>(ErrorMessage.PasswordRequired);

			if (request.Password.Length < 4)
				return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidPasswordFormat);

			if (String.IsNullOrWhiteSpace(request.Email))
				return CreateErrorResponse<ResponseBase>(ErrorMessage.EmailRequired);

			if (!IsValidEmail(request.Email))
				return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidEmailFormat);


			if (_unitOfWork.UserRepository.Count(u => u.Email == request.Email) > 0)
				return CreateErrorResponse<ResponseBase>(ErrorMessage.EmailExists);

			return new ResponseBase
			{
				StatusCode = StatusCode.Created,
			};
		}

		/// <summary>
		/// Execute change password request validating
		/// </summary>
		public ResponseBase IsValidChangePasswordRequest(ChangePasswordRequest request)
		{
			if (String.IsNullOrWhiteSpace(request.NewPassword))
				return CreateErrorResponse<ResponseBase>(ErrorMessage.PasswordRequired);

			if (request.NewPassword.Length < 4)
				return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidPasswordFormat);

			var passwordHash = _passwordSecurityHandler.HashPassword(request.NewPassword);
			if (_passwordSecurityHandler.VerifyPassword(new PasswordSecurityRequest
			{
				Password = request.Password,
				HashedPassword = passwordHash
			}))
				return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidOldPassword);

			return new ResponseBase
			{
				StatusCode = StatusCode.Created,
			};
		}


		/// <summary>
		/// Validating email by regex
		/// </summary>
		private bool IsValidEmail(string email)
		{
			string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
			return Regex.IsMatch(email, emailPattern);
		}
	}
}