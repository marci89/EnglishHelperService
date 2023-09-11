namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Password Verify request
	/// </summary>
	public class PasswordSecurityRequest
	{
		public string HashedPassword { get; set; }
		public string Password { get; set; }
	}
}
