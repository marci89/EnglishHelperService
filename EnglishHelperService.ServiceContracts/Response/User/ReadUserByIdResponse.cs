namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Read by id user response
	/// </summary>
	public class ReadUserByIdResponse : ResponseBase
	{
		public User Result { get; set; }
	}
}
