namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Update user request
	/// </summary>
	public class UpdateUserRequest
	{
		public long Id { get; set; }
		public string Username { get; set; }
	}
}
