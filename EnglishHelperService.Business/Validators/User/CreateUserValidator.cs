using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Validating user create request
	/// </summary>
	public class CreateUserValidator : BaseValidator<CreateUserResponse>
	{

		/// <summary>
		/// Execute user create validating
		/// </summary>
		public CreateUserResponse IsValid(CreateUserRequest request)
		{

			if (request is null)
				return CreateErrorResponse(ErrorMessage.InvalidRequest);

			if (String.IsNullOrWhiteSpace(request.Username))
				return CreateErrorResponse(ErrorMessage.UsernameRequired);

			if (request.Username.Length > 50)
				return CreateErrorResponse(ErrorMessage.UsernameMaxLength);

			if (String.IsNullOrWhiteSpace(request.Password))
				return CreateErrorResponse(ErrorMessage.PasswordRequired);

			if (request.Password.Length < 4)
				return CreateErrorResponse(ErrorMessage.InvalidPasswordFormat);

			if (String.IsNullOrWhiteSpace(request.Email))
				return CreateErrorResponse(ErrorMessage.EmailRequired);

			return new CreateUserResponse
			{
				StatusCode = StatusCode.Created,
			};
		}
	}
}