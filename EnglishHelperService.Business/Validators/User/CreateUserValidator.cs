using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Validating user create request
	/// </summary>
	public class CreateUserValidator
	{

		/// <summary>
		/// Execute user create validating
		/// </summary>
		public CreateUserResponse IsValid(CreateUserRequest request)
		{

			if (request is null)
				return new CreateUserResponse
				{
					StatusCode = StatusCode.BAD_REQUEST,
					ErrorMessage = ErrorMessage.INVALID_REQUEST
				};

			if (String.IsNullOrEmpty(request.Username))
				return new CreateUserResponse
				{
					StatusCode = StatusCode.BAD_REQUEST,
					ErrorMessage = ErrorMessage.USERNAME_REQUIRED
				};

			if (String.IsNullOrEmpty(request.Password))
				return new CreateUserResponse
				{
					StatusCode = StatusCode.BAD_REQUEST,
					ErrorMessage = ErrorMessage.PASSWORD_REQUIRED
				};

			if (String.IsNullOrEmpty(request.Email))
				return new CreateUserResponse
				{
					StatusCode = StatusCode.BAD_REQUEST,
					ErrorMessage = ErrorMessage.EMAIL_REQUIRED
				};

			return new CreateUserResponse
			{
				StatusCode = StatusCode.CREATED,
			};
		}
	}
}