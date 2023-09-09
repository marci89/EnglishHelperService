namespace EnglishHelperService.ServiceContracts
{
	public enum ErrorMessage
	{
		InvalidRequest,
		ServerError,
		NotFound,
		DeleteFailed,
		EditFailed,

		InvalidPasswordOrUsernameOrEmail,
		UsernameOrEmailRequired,
		UsernameRequired,
		UsernameMaxLength,
		UsernameExists,
		PasswordRequired,
		InvalidPasswordFormat,
		EmailRequired,
		EmailExists,
		InvalidEmailFormat
	}
}