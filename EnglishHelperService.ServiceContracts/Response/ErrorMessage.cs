namespace EnglishHelperService.ServiceContracts
{
	public enum ErrorMessage
	{
		InvalidRequest,
		ServerError,

		InvalidPasswordOrUsername,
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