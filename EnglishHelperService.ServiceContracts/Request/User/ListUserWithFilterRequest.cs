

namespace EnglishHelperService.ServiceContracts
{
	public class ListUserWithFilterRequest : PagedListRequest
	{
		public string Username { get; set; }
		public string Email { get; set; }
	}
}
