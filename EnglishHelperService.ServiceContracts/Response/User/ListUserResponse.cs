namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// List paging users response 
	/// </summary>
	public class ListUserResponse : ResponseBase
	{
		public PagedList<User> Result { get; set; }
	}
}
