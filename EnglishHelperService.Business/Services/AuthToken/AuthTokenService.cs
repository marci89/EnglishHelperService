using EnglishHelperService.Business.Settings;
using EnglishHelperService.Persistence.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglishHelperService.Business
{
    /// <summary>
    /// JWT token generaton service for auth
    /// </summary>
    public class AuthTokenService : IAuthTokenService
    {
        /// <summary>
        /// Key for token. It is got from appsettings file "TokenKey"
        /// </summary>
        private readonly SymmetricSecurityKey _key;

        private readonly ISecuritySettings _settings;

        public AuthTokenService(ISecuritySettings settings)
        {
            _settings = settings;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.TokenKey));
        }

        /// <summary>
        /// Generating a JWT token for loginUser
        /// </summary>
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };


            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_settings.TokenExpirationDays),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}