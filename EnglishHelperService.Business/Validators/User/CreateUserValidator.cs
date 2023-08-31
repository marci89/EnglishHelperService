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

			if (String.IsNullOrEmpty(request.Username))
				return CreateErrorResponse(ErrorMessage.UsernameRequired);

			if (request.Username.Length > 50)
				return CreateErrorResponse(ErrorMessage.UsernameMaxLength);

			if (String.IsNullOrEmpty(request.Password))
				return CreateErrorResponse(ErrorMessage.PasswordRequired);


			if (String.IsNullOrEmpty(request.Email))
				return CreateErrorResponse(ErrorMessage.EmailRequired);

			return new CreateUserResponse
			{
				StatusCode = StatusCode.Created,
			};
		}
	}
}