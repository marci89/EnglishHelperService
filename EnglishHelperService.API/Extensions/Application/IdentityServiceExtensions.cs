using EnglishHelperService.Business.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EnglishHelperService.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        /// <summary>
        /// Handle JWT token authentication
        /// </summary>
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, ISecuritySettings settings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                            .UTF8.GetBytes(settings.TokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }
    }
}
