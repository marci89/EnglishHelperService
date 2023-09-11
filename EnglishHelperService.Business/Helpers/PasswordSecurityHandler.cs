using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// BCrypt hash generator and validator
	/// </summary>
	public class PasswordSecurityHandler
	{
		public string HashPassword(string password)
		{
			// Hash the password using BCrypt and generate a new salt.
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool VerifyPassword(PasswordSecurityRequest request)
		{
			// Verify the entered password against the stored hashed password.
			return BCrypt.Net.BCrypt.Verify(request.Password, request.HashedPassword);
		}
	}
}
