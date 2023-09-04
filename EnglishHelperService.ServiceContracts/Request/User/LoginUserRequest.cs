namespace EnglishHelperService.ServiceContracts
{
	public class LoginUserRequest
	{
		/// <summary>
		/// It could be email or username
		/// </summary>
		public string Identifier { get; set; }
		public string Password { get; set; }
	}
}
