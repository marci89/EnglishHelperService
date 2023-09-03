using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	/// Alap interface repository-k számára.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Menti az entitás(ok)on történt módosítás(oka)t.
		/// </summary>
		Task SaveAsync();
	}

	/// <summary>
	/// Alap interface olyan repository számára, amely egy meghatározott entitás halmaz lekérdezésével és módosításával foglalkozik.
	/// </summary>
	/// <typeparam name="TEntity">az entitás típusa</typeparam>
	public interface IRepository<TEntity> : IRepository, IReadOnlyRepository<TEntity> where TEntity : class, new()
	{
		/// <summary>
		/// Új rekordként menti az átadott entitást.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		Task CreateAsync(TEntity entity);

		/// <summary>
		/// Új rekordokként menti az átadott entitásokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		Task CreateManyAsync(IEnumerable<TEntity> entities);

		/// <summary>
		/// Menti az átadott entitáson történt módosításokat.
		/// </summary>
		/// <param name="entity">a mentendő entitás</param>
		Task UpdateAsync(TEntity entity);

		/// <summary>
		/// Új értékeket állít be a feltételeknek megfelelő entitásoknak.
		/// </summary>
		/// <param name="newValues">új értékek</param>
		/// <param name="where">szűrőfeltétel a frissítendő elemekre</param>
		Task UpdateAsync(Expression<Func<TEntity, TEntity>> newValues, Expression<Func<TEntity, bool>> where = null);

		/// <summary>
		/// Menti az átadott entitásokon történt módosításokat.
		/// </summary>
		/// <param name="entities">a mentendő entitások</param>
		Task UpdateManyAsync(IEnumerable<TEntity> entities);

		/// <summary>
		/// Törli az adott entitást.
		/// </summary>
		/// <param name="entity">a törlendő entitás</param>
		Task DeleteAsync(TEntity entity);

		/// <summary>
		/// Törli az adott feltételeknek megfelelő entitásokat.
		/// </summary>
		/// <param name="where">szűrőfeltétel a törlendő elemekre</param>
		Task DeleteAsync(Expression<Func<TEntity, bool>> where);

		/// <summary>
		/// Törli az adott entitásokat.
		/// </summary>
		/// <param name="entities">a törlendő entitások</param>
		Task DeleteManyAsync(IEnumerable<TEntity> entities);
	}
}