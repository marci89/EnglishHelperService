namespace EnglishHelperService.ServiceContracts
{
	public class ListUserResponse : ResponseBase
	{
		public PagedList<User> Result { get; set; }
	}
}
