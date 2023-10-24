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
        /// Execute two password match validating
        /// </summary>
        public ResponseBase IsValidPasswordMatch(long userId, string password)
        {
            var user = _unitOfWork.UserRepository.Query(u => u.Id == userId).FirstOrDefault();
            if (user is null) return CreateErrorResponse<ResponseBase>(ErrorMessage.NotFound, StatusCode.NotFound);

            if (!_passwordSecurityHandler.VerifyPassword(new PasswordSecurityRequest
            {
                Password = password,
                HashedPassword = user.Password
            }))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidOldPassword);

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }


        /// <summary>
        /// Execute user create request validating
        /// </summary>
        public CreateUserResponse IsValidCreateRequest(CreateUserRequest request)
        {
            if (request is null)
                return CreateErrorResponse<CreateUserResponse>(ErrorMessage.InvalidRequest);

            var usernameValidation = IsValidUsername(request.Username);
            if (usernameValidation.HasError && usernameValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<CreateUserResponse>(usernameValidation.ErrorMessage.Value);
            }

            var passwordValidation = IsValidPassword(request.Password);
            if (passwordValidation.HasError && passwordValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<CreateUserResponse>(passwordValidation.ErrorMessage.Value);
            }

            var emailValidation = IsValidEmail(request.Email);
            if (emailValidation.HasError && emailValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<CreateUserResponse>(emailValidation.ErrorMessage.Value);
            }

            return new CreateUserResponse
            {
                StatusCode = StatusCode.Created,
            };
        }

        /// <summary>
        /// Execute user update request validating
        /// </summary>
        public ResponseBase IsValidUpdateRequest(UpdateUserRequest request)
        {
            if (request is null)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidRequest);

            var usernameValidation = IsValidUsername(request.Username);
            if (usernameValidation.HasError && usernameValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(usernameValidation.ErrorMessage.Value);
            }

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute change email request validating
        /// </summary>
        public ResponseBase IsValidChangeEmailRequest(ChangeEmailRequest request, long userId)
        {
            if (request is null)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidRequest);

            var passwordValidation = IsValidPassword(request.Password);
            if (passwordValidation.HasError && passwordValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(passwordValidation.ErrorMessage.Value);
            }

            var passwordMatchValidation = IsValidPasswordMatch(userId, request.Password);
            if (passwordMatchValidation.HasError && passwordMatchValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidPasswordOrEmail);
            }

            var emailValidation = IsValidEmail(request.Email);
            if (emailValidation.HasError && emailValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(emailValidation.ErrorMessage.Value);
            }

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute change password request validating
        /// </summary>
        public ResponseBase IsValidChangePasswordRequest(ChangePasswordRequest request, long userId)
        {
            if (request is null)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidRequest);

            var passwordValidation = IsValidPassword(request.NewPassword);
            if (passwordValidation.HasError && passwordValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(passwordValidation.ErrorMessage.Value);
            }

            var passwordMatchValidation = IsValidPasswordMatch(userId, request.Password);
            if (passwordMatchValidation.HasError && passwordMatchValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(passwordMatchValidation.ErrorMessage.Value);
            }

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute forgot password request validating
        /// </summary>
        public ForgotPasswordResponse IsValidForgotPasswordRequest(ForgotPasswordRequest request)
        {

            if (request is null)
                return CreateErrorResponse<ForgotPasswordResponse>(ErrorMessage.InvalidRequest);

            if (String.IsNullOrWhiteSpace(request.Identifier))
                return CreateErrorResponse<ForgotPasswordResponse>(ErrorMessage.UsernameOrEmailRequired);

            return new ForgotPasswordResponse
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute password validating
        /// </summary>
        public ResponseBase IsValidPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.PasswordRequired);

            if (password.Length < 4)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidPasswordFormat);


            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute account delete request validating
        /// </summary>
        public ResponseBase IsValidDeleteAccountRequest(DeleteAccountRequest request)
        {

            if (request is null)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidRequest);

            if (String.IsNullOrWhiteSpace(request.Identifier))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.UsernameOrEmailRequired);

            if (String.IsNullOrWhiteSpace(request.Password))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.PasswordRequired);

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }



        #region Private methods

        /// <summary>
        /// Execute username validating
        /// </summary>
        private ResponseBase IsValidUsername(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.UsernameRequired);

            if (username.Length > 50)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.UsernameMaxLength);

            if (_unitOfWork.UserRepository.Count(u => u.Username == username) > 0)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.UsernameExists);

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }


        /// <summary>
        /// Execute email validating
        /// </summary>
        private ResponseBase IsValidEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.EmailRequired);

            if (!IsValidEmailFormat(email))
                return CreateErrorResponse<ResponseBase>(ErrorMessage.InvalidEmailFormat);


            if (_unitOfWork.UserRepository.Count(u => u.Email == email) > 0)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.EmailExists);

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Validating email by regex
        /// </summary>
        private bool IsValidEmailFormat(string email)
        {
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, emailPattern);
        }

        #endregion
    }
}