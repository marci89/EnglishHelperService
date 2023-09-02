using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Validating user login request
	/// </summary>
	public class LoginUserValidator : BaseValidator<LoginUserResponse>
	{

		/// <summary>
		/// Execute user login validating
		/// </summary>
		public LoginUserResponse IsValid(LoginUserRequest request)
		{

			if (request is null)
				return CreateErrorResponse(ErrorMessage.InvalidRequest);

			if (String.IsNullOrWhiteSpace(request.Username))
				return CreateErrorResponse(ErrorMessage.UsernameRequired);

			if (String.IsNullOrWhiteSpace(request.Password))
				return CreateErrorResponse(ErrorMessage.PasswordRequired);

			return new LoginUserResponse
			{
				StatusCode = StatusCode.Ok,
			};
		}
	}
}