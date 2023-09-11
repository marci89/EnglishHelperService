namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Logined user response
	/// </summary>
	public class LoginUserResponse : ResponseBase
	{
		public LoginUser Result { get; set; }
	}
}
