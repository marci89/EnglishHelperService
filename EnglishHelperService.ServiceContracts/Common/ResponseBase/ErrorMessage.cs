namespace EnglishHelperService.ServiceContracts
{
	public enum ErrorMessage
	{
		InvalidRequest,
		ServerError,
		NotFound,
		DeleteFailed,
		EditFailed,
		CreateFailed,

		InvalidPasswordOrUsernameOrEmail,
		UsernameOrEmailRequired,
		UsernameRequired,
		UsernameMaxLength,
		UsernameExists,
		PasswordRequired,
		InvalidPasswordFormat,
		InvalidOldPassword,
		EmailRequired,
		EmailExists,
		InvalidEmailFormat
	}
}