﻿namespace EnglishHelperService.ServiceContracts
{
	public class User
	{
		public long Id { get; set; }
		public string Role { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
	}
}
