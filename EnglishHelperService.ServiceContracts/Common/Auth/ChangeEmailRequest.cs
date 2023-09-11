namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// User email change request
	/// </summary>
	public class ChangeEmailRequest
	{
		public long Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}