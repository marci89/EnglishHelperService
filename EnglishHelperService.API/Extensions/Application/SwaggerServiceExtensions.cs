using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EnglishHelperService.API.Extensions
{
	public static class SwaggerServiceExtensions
	{
		/// <summary>
		/// Handle Swagger registration
		/// </summary>
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version;

			// Add Swagger generation
			services.AddSwaggerGen(c =>
				{
					c.SwaggerDoc("v1", new OpenApiInfo
					{
						Title = "English Helper Service",
						Version = $"v{version}",
						Description = "English Helper Service",
						Contact = new OpenApiContact
						{
							Email = "kismarczirobi@gmail.com",
							Name = "József Kismarczi"
						}
					});

					// Add the security definition for bearer tokens
					c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
					{
						Name = "Authorization",
						Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.ApiKey,
						Scheme = "Bearer"
					});
					// Add the security Requirement for bearer tokens
					c.AddSecurityRequirement(new OpenApiSecurityRequirement
					{
					{
						new OpenApiSecurityScheme
						{
							Name = "Bearer",
							In = ParameterLocation.Header,
							Reference = new OpenApiReference
							{
								Id = "Bearer",
								Type =ReferenceType.SecurityScheme
							}
						},
						new List<string>()
					}
					});
				});
			return services;
		}
	}
}
