namespace EnglishHelperService.Business.Models
{
	public class User
	{
		public long Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTime Created { get; set; }
	}
}
