namespace EnglishHelperService.ServiceContracts
{
	public class PasswordSecurityRequest
	{
		public string HashedPassword { get; set; }
		public string Password { get; set; }
	}
}
