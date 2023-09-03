using System.Linq.Expressions;

namespace EnglishHelperService.Persistence.Common
{
	/// <summary>
	/// Alap interface olyan repository számára, amely különböző entitás halmaz lekérdezésével foglalkozik.
	/// </summary>
	public interface IReadOnlyRepository
	{
		/// <summary>
		/// Aszinkron módon kiolvassa az adott feltételnek megfelelő első entitást.
		/// </summary>
		/// <param name="where">szűrőfeltétel</param>
		/// <returns>adott típusú entitás példány, vagy null, ha nem létezik ilyen</returns>
		Task<TEntity> ReadAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class, new();

		/// <summary>
		/// Készít egy lekérdező objektumot az összes rekordra.
		/// </summary>
		/// <returns>lekérdező objektum</returns>
		IQueryable<TEntity> Query<TEntity>() where TEntity : class, new();

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new();

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz a megadott sorrendben.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, out long totalCount) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount) where TEntity : class, new();

		/// <summary>
		/// Aszinkron módon lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		IQueryable<TEntity> PagedQuery<TEntity>(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount) where TEntity : class, new();

		/// <summary>
		///  Összeszámolja az adott szűrő kifejezésnek megfelelő entitásokat.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>a szűrő kifejezésnek megfelelő entitások száma</returns>
		long Count<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, new();
	}




	/// <summary>
	/// Alap interface olyan repository számára, amely egy meghatározott entitás halmaz lekérdezésével foglalkozik.
	/// </summary>
	/// <typeparam name="TEntity">az entitás típusa</typeparam>
	public interface IReadOnlyRepository<TEntity> where TEntity : class, new()
	{
		/// <summary>
		/// A lekérdezhető teljes entitás halmaz.
		/// </summary>
		IQueryable<TEntity> EntitySet { get; }

		/// <summary>
		/// Aszinkron módon kiolvassa az adott feltételnek megfelelő első entitást.
		/// </summary>
		/// <param name="where">szűrőfeltétel</param>
		/// <returns>adott típusú entitás példány, vagy null, ha nem létezik ilyen</returns>
		Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> where);

		/// <summary>
		/// Készít egy lekérdező objektumot az összes rekordra.
		/// </summary>
		/// <returns>lekérdező objektum</returns>
		IQueryable<TEntity> Query();

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);

		/// <summary>
		/// Készít egy lekérdező objektumot az adott szűrő kifejezésnek megfelelő entitásokhoz a megadott sorrendben.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <returns>lekérdező objektum</returns>
		IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, out long totalCount);

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, out long totalCount);

		/// <summary>
		/// Lapozható listák kiszolgálására készült lekérdező függvény
		/// </summary>
		/// <typeparam name="TEntity">entitás típusa</typeparam>
		/// <param name="pageNumber">lekérendő oldal száma (1-től kezdődően)</param>
		/// <param name="itemsOnPage">elemek száma egy oldalon</param>
		/// <param name="filter">szűrő kifejezés</param>
		/// <param name="orderBy">rendezési kifejezés</param>
		/// <param name="totalCount">összes elemszám</param>
		/// <returns></returns>
		IQueryable<TEntity> PagedQuery(int pageNumber, int itemsOnPage, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out long totalCount);

		/// <summary>
		/// Aszinkron módon összeszámolja az adott szűrő kifejezésnek megfelelő entitásokat.
		/// </summary>
		/// <param name="filter">szűrő kifejezés</param>
		/// <returns>a szűrő kifejezésnek megfelelő entitások száma</returns>
		long Count(Expression<Func<TEntity, bool>> filter = null);
	}
}
