namespace EnglishHelperService.ServiceContracts
{
	public class LoginUser
	{
		public string Username { get; set; }
		public RoleType Role { get; set; }
		public string Token { get; set; }
	}
}
