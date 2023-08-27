using EnglishHelperService.Business.Models;
using System.Security.Cryptography;
using System.Text;

namespace EnglishHelperService.Business
{
	public class PasswordSecurityHandler
	{
		public PasswordSecurityResponse CreatePassword(string password)
		{
			using var hmac = new HMACSHA512();
			return new PasswordSecurityResponse
			{
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
				PasswordSalt = hmac.Key
			};
		}

		public bool IsValidPassword(PasswordSecurityRequest request)
		{
			using var hmac = new HMACSHA512(request.PasswordSalt);

			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

			for (int i = 0; i < computedHash.Length; i++)
			{
				if (computedHash[i] != request.PasswordHash[i]) return false;
			}
			return true;
		}
	}
}
