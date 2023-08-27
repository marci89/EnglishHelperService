namespace EnglishHelperService.Business.Models
{
	public class PasswordSecurityResponse
	{
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
	}
}
