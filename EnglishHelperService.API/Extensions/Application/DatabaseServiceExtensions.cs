using EnglishHelperService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.API.Extensions
{
	public static class DatabaseServiceExtensions
	{
		/// <summary>
		/// Handle database registration
		/// </summary>
		public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			});
			return services;
		}
	}
}
