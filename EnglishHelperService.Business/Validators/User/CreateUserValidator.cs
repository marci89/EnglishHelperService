using EnglishHelperService.Persistence.Common;
using EnglishHelperService.ServiceContracts;
using System.Text.RegularExpressions;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Validating user create request
	/// </summary>
	public class CreateUserValidator : BaseValidator<CreateUserResponse>
	{
		private readonly IUnitOfWork _unitOfWork;

		public CreateUserValidator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Execute user create validating
		/// </summary>
		public CreateUserResponse IsValid(CreateUserRequest request)
		{

			if (request is null)
				return CreateErrorResponse(ErrorMessage.InvalidRequest);

			if (String.IsNullOrWhiteSpace(request.Username))
				return CreateErrorResponse(ErrorMessage.UsernameRequired);

			if (_unitOfWork.UserRepository.Count(u => u.Username == request.Username) > 0)
				return CreateErrorResponse(ErrorMessage.UsernameExists);

			if (request.Username.Length > 50)
				return CreateErrorResponse(ErrorMessage.UsernameMaxLength);

			if (String.IsNullOrWhiteSpace(request.Password))
				return CreateErrorResponse(ErrorMessage.PasswordRequired);

			if (request.Password.Length < 4)
				return CreateErrorResponse(ErrorMessage.InvalidPasswordFormat);

			if (String.IsNullOrWhiteSpace(request.Email))
				return CreateErrorResponse(ErrorMessage.EmailRequired);

			if (!IsValidEmail(request.Email))
				return CreateErrorResponse(ErrorMessage.InvalidEmailFormat);


			if (_unitOfWork.UserRepository.Count(u => u.Email == request.Email) > 0)
				return CreateErrorResponse(ErrorMessage.EmailExists);

			return new CreateUserResponse
			{
				StatusCode = StatusCode.Created,
			};
		}

		private bool IsValidEmail(string email)
		{
			string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
			return Regex.IsMatch(email, emailPattern);
		}
	}
}