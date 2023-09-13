namespace EnglishHelperService.Business.Settings
{

	/// <summary>
	/// Interface describing database settings
	/// </summary>
	public interface IDatabaseSettings
	{
		/// <summary>
		/// Database server type
		/// </summary>
		DatabaseProvider Provider { get; }

		/// <summary>
		/// Connection String for database
		/// </summary>
		string ConnectionString { get; }

		/// <summary>
		/// Whether to enable automatic database schema update (migration update)
		/// </summary>
		bool AutoMigrationEnabled { get; }
	}
}