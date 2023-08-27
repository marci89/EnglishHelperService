using EnglishHelperService.Persistence.Repositories;

namespace EnglishHelperService.Persistence.UnitOfWork
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
		void Save();

		/// <summary>
		/// Véglegesíti a módosításokat az adatbázison (ment és kommittál).
		/// </summary>
		void Commit();

		/// <summary>
		/// Visszagörgeti a módosításokat az adatbázison.
		/// </summary>
		void Rollback();
	}
}
