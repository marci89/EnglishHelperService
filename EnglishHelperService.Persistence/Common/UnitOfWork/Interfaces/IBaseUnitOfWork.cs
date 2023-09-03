namespace EnglishHelperService.Persistence.Common
{
	public interface IBaseUnitOfWork : IDisposable
	{
		/// <summary>
		/// Adatbázis tranzakciót indít.
		/// </summary>
		void BeginTransaction();

		/// <summary>
		/// Menti az adott pontig elvégzett módosításokat az adatbázisba, de tranzakciót nem kommittál.
		/// </summary>
		Task SaveAsync();

		/// <summary>
		/// Véglegesíti a módosításokat az adatbázison (ment és kommittál).
		/// </summary>
		Task CommitAsync();

		/// <summary>
		/// Visszagörgeti a módosításokat az adatbázison.
		/// </summary>
		void Rollback();
	}
}