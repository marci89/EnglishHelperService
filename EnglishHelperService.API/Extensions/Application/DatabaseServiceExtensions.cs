using EnglishHelperService.Business.Settings;
using EnglishHelperService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.API.Extensions
{
	public static class DatabaseServiceExtensions
	{
		/// <summary>
		/// Handle database registration
		/// </summary>
		public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IDatabaseSettings settings)
		{
			switch (settings.Provider)
			{
				//MSSQL
				case DatabaseProvider.MSSQL:
					services.AddDbContext<DataContext>(options =>
					{
						options.UseSqlServer(settings.ConnectionString);
					});

					return services;

				//PostgreSQL
				case DatabaseProvider.PostgreSQL:
					services.AddDbContext<DataContext>(options =>
					{
						options.UseNpgsql(settings.ConnectionString);
					});
					return services;

				//SQLite
				case DatabaseProvider.SQLite:
					services.AddDbContext<DataContext>(options =>
					{
						options.UseSqlite(settings.ConnectionString);
					});

					return services;

				default:
					throw new ArgumentException("Not supported database type.");

			}
		}
	}
}
