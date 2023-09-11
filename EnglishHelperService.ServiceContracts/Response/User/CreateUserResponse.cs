namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Create user response
	/// </summary>
	public class CreateUserResponse : ResponseBase
	{
		public User Result { get; set; }
	}
}
