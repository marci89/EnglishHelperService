namespace EnglishHelperService.ServiceContracts
{
	public enum ErrorMessage
	{
		InvalidRequest,
		ServerError,

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