namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// User password change request
	/// </summary>
	public class ChangePasswordRequest
	{
		public long Id { get; set; }
		public string Password { get; set; }
		public string NewPassword { get; set; }
	}
}
