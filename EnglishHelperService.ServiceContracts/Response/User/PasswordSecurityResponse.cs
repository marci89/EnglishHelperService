namespace EnglishHelperService.ServiceContracts
{
	public class PasswordSecurityResponse
	{
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
	}
}
