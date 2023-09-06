namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	/// Interface describing the integration of database connection services.
	/// </summary>
	public interface IUnitOfWorkBase : IDisposable
	{
		/// <summary>
		/// Starts a database transaction.
		/// </summary>
		void BeginTransaction();

		/// <summary>
		/// It saves the changes made up to that point in the database, but you have not committed a transaction.
		/// </summary>
		Task SaveAsync();

		/// <summary>
		/// Finalizes changes to the database (saved and committed).
		/// </summary>
		Task CommitAsync();

		/// <summary>
		/// Rolls back changes on the database.
		/// </summary>
		void Rollback();
	}
}