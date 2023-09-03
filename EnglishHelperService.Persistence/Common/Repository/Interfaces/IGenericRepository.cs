using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{

	/// <summary>
	/// Alap interface olyan repository számára, amely egy meghatározott entitás halmaz lekérdezésével foglalkozik.
	/// </summary>
	/// <typeparam name="TEntity">az entitás típusa</typeparam>
	public interface IGenericRepository : IReadOnlyRepository
	{
		/// <summary>
		/// Aszinkron módon új rekordként menti az átadott entitást.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		Task CreateAsync<TEntity>(TEntity entity) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon új rekordokként menti az átadott entitásokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		Task CreateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon menti az átadott entitáson történt módosításokat.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon új értékeket állít be a feltételeknek megfelelő entitásoknak.
		/// </summary>
		/// <param name="newValues">új értékek</param>
		/// <param name="where">szűrőfeltétel a frissítendő elemekre</param>
		Task UpdateAsync<TEntity>(
			 Expression<Func<TEntity, TEntity>> newValues,
			 Expression<Func<TEntity, bool>> where = null
		 ) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon menti az átadott entitásokon történt módosításokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon törli az adott entitást.
		/// </summary>
		/// <param name="entity">a törlendő entitás</param>
		Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon törli az adott feltételeknek megfelelő entitásokat.
		/// </summary>
		/// <param name="where">szűrőfeltétel a törlendő elemekre</param>
		Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon törli az adott entitásokat.
		/// </summary>
		/// <param name="entities">a törlendő entitások</param>
		Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon menti az entitás(ok)on történt módosítás(oka)t.
		/// </summary>
		Task SaveAsync();
	}
}