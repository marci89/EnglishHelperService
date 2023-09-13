using Microsoft.Extensions.Configuration;


namespace EnglishHelperService.Business.Settings
{
	/// <summary>
	/// Department responsible for database settings
	/// </summary>
	public class DatabaseSettings : IDatabaseSettings
	{
		public DatabaseSettings(IConfiguration configuration)
		{
			Provider = configuration.GetSection("DatabaseSettings").GetValue<DatabaseProvider>("Provider");
			ConnectionString = configuration.GetSection("DatabaseSettings").GetValue<string>("ConnectionString");
			AutoMigrationEnabled = configuration.GetSection("DatabaseSettings").GetValue<bool>("AutoMigrationEnabled");

		}

		/// <summary>
		/// Database server type
		/// </summary>
		public virtual DatabaseProvider Provider { get; private set; }

		/// <summary>
		/// Connection String for database
		/// </summary>
		public virtual string ConnectionString { get; private set; }

		/// <summary>
		/// Whether to enable automatic database schema update (migration update)
		/// </summary>
		public virtual bool AutoMigrationEnabled { get; private set; }
	}
}